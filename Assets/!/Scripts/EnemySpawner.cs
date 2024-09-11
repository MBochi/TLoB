using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float respawnTime;
    private bool isSpawning;
    // Update is called once per frame
    void Update()
    {      
        if (!isSpawning)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        isSpawning = !isSpawning;
        Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(respawnTime);
        isSpawning = !isSpawning;
    }
}
