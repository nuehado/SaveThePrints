using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,120f)] [SerializeField] private float secondsBetweenSpawns = 1f;
    private int enemyCount = 0;
    [SerializeField] private int maxEnemies = 3;
    [SerializeField] private GameObject enemy;
    [SerializeField] private PlayableDirector printTimeline;
    [SerializeField] private AudioSource printSound;

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
            printTimeline.Play();
            printSound.Play();
        }
        
    }
}
