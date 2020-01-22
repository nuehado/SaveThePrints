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
    [SerializeField] private PlayableDirector extruderTimeline;
    [SerializeField] private Animator zAssemblyAnimator;
    [SerializeField] private AudioSource printSound;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        

    }

    private IEnumerator SpawnEnemy()
    {
        if (enemyCount >= maxEnemies)
        {
            extruderTimeline.Pause();

            
            StopCoroutine(SpawnEnemy());
            zAssemblyAnimator.speed = 0f; // todo this isn't stopped the right way
        }
        else
        {
            extruderTimeline.Play();
            zAssemblyAnimator.speed = 1f / secondsBetweenSpawns;
            zAssemblyAnimator.Play(0);
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
