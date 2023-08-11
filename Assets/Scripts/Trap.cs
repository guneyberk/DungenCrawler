using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [ContextMenu("Trigger Trap")]
    public void TriggerTrap()
    {
        foreach(var  enemy in _enemies)
            enemy.Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null )
            _enemies.Add(enemy);
    } 
    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null )
            _enemies.Remove(enemy);
    }
}
