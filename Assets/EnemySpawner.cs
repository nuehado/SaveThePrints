using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float secondsBetweenSpawns = 1f;
    private int enemyCount = 0;
    [SerializeField] private int maxEnemies = 3;
    [SerializeField] private GameObject enemy;
    void Start()
    {
        StartCoroutine(SpawnEnemy(0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnEnemy(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(enemy);
        Debug.Log("enemy spawned at: " + Time.time);
        enemyCount ++;
        if (enemyCount < maxEnemies)
        {
            StartCoroutine(SpawnEnemy(secondsBetweenSpawns));
        }
        
    }
}
