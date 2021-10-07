using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawer : Spawner
{
    [SerializeField] protected Vector3 spawnPosition;

    protected override void Awake()
    {
        base.Awake();
        StartSpawn();
        GameManager.OnGameRestart += StartSpawn;
    }

    protected override IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(prefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnRatio);
        }
    }
}
