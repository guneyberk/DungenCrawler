using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int _maxHealth = 10;

    int _health;
    private RaycastHit[] _results = new RaycastHit[100];
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Enemy _target;
    private float _attackDistance = 1.5f;
    private float _attackTime = 3f;
    //private float _nextAttackTime;
    private float _attackDelay = 3f;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        _health = _maxHealth;
    }


    public void TakeDamage(int amount)
    {
        _maxHealth -= amount;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Update()
    {

        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
        else if (_target != null)
        {
            if(ShouldAttackTarget())
                Attack();
            MoveToTarget();
        }
    }

    private bool ShouldAttackTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);

        if (distanceToTarget > _attackDistance)
            return false;

        if (Time.time < _attackTime)
        {
            return false;
        }
        return true;
    }

    private void MoveToTarget()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
    }

    private void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int hits = Physics.RaycastNonAlloc(ray, _results);
        if (TrySetEnemyTarget(hits))
            return;
        TrySetGroundTarget(hits);
    }

    private bool TrySetEnemyTarget(int hits)
    {
        for (int i = 0; i < hits; i++)
        {
            var enemy = _results[i].collider.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                _target = enemy;
                return true;
            }
        }
        return false;
    }

    private void TrySetGroundTarget(int hits)
    {
        for (int i = 0; i < hits; i++)
        {
            if (_navMeshAgent.SetDestination(_results[i].point))
            {
                _target = null;
                break;
            }
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        _attackTime = Time.time + _attackDelay;
        _target.Die();
    }

    private void Die()
    {
        //Debug.Log("here");
        SceneManager.LoadScene(0);
    }
}
