using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,120f)] [SerializeField] private float secondsBetweenSpawns = 1f;
    private int enemyCount = 0;
    [SerializeField] private int maxEnemies = 3;
    [SerializeField] private GameObject enemy;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity, FindObjectOfType<EnemySpawner>().transform);
        enemyCount++;
        yield return new WaitForSeconds(secondsBetweenSpawns);
        if (enemyCount < maxEnemies)
        {
            StartCoroutine(SpawnEnemy());
        }
        
    }
}
