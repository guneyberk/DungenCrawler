using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator _animation;
    private NavMeshAgent _navMeshAgent;
    private Player _player;
    private float _nextAttackTime;


    [SerializeField] float _attackDistacne = 2;
    [SerializeField] float _attackDelay = 3;
    [SerializeField] private int _attackDamage=1;
    [SerializeField] private int _launchPower=3;

    private void Awake()
    {
        _animation = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        GetComponent<NavMeshAgent>().SetDestination(_player.transform.position);
    }
    private void Update()
    {
        _animation.SetFloat("Speed", _navMeshAgent.velocity.magnitude);

        if (ReadyToAttack())
        {
            Attack();
        }
    }

    private void Attack()
    {
        _player.TakeDamage(_attackDamage);
        _nextAttackTime = Time.time + _attackDelay;
        _animation.SetTrigger("Attack");
    }

    private bool ReadyToAttack()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);
        if (distanceToTarget > _attackDistacne)
        {
            return false;
        }
        if (Time.time < _nextAttackTime)
            return false;
        return true;
    }

    [ContextMenu("Die")]
    public void Die()
    {
        _navMeshAgent.enabled = false;
        _animation.enabled = false;
        var launchVelocity = -transform.position + transform.up;
        var rigidbodies = GetComponentsInChildren<Rigidbody>();

        //launchVelocity *= _launchPower;

        foreach(var rb in rigidbodies) 
        { rb.velocity = launchVelocity;}
    }
}


