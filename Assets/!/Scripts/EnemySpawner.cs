using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float respawnTime;
    [SerializeField] private float maxEnemies;
    private bool isSpawning = false;
    public bool activateSpawner = false;

    void Update()
    {
        if (activateSpawner)
        {
            if (!isSpawning)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            isSpawning = true;
            Instantiate(enemyPrefab, this.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-1.5f, 1.5f), 0f), Quaternion.identity);
            StartCoroutine(Timer());
        }
        else
        {
            isSpawning = false;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(respawnTime);
        isSpawning = false;
    }
}
