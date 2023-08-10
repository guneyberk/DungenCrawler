using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    float _nextSpawnTime;

    [SerializeField] float _spawnDelay = 7f;
    [SerializeField] GameObject _prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ReadyToSpawn())
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _nextSpawnTime = Time.time + _spawnDelay;
        Instantiate(_prefab,transform.position, Quaternion.identity);
    }

    private bool ReadyToSpawn() => Time.time >= _nextSpawnTime;
}
