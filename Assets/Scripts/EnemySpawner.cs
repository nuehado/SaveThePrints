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
        if (enemyCount >= maxEnemies)
        {
            printTimeline.Pause();
            StopCoroutine(SpawnEnemy());
        }
        else
        {
            printTimeline.Play(); // todo time of animation and sound should be dependant on spawntime
        }

        //printSound.Play();
        yield return new WaitForSeconds(secondsBetweenSpawns);
        if (enemyCount < maxEnemies)
        {
            enemyCount++;
            Instantiate(enemy, transform.position, Quaternion.identity, FindObjectOfType<EnemySpawner>().transform);
            StartCoroutine(SpawnEnemy());
            
        }
        
    }
}
