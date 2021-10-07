using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected float spawnPadding;
    protected Coroutine spawnCoroutine;
    protected float spawnRatio;

    protected virtual void Awake()
    {
        spawnRatio = spawnPadding / GameSettings.GameSpeed;
        GameManager.OnGameOver += StopSpawn;
    }

    protected void StartSpawn()
    {
        spawnCoroutine = StartCoroutine(Spawn());
    }

    protected void StopSpawn()
    {
        StopCoroutine(spawnCoroutine);
    }

    protected abstract IEnumerator Spawn();
}
