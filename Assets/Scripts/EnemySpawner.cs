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
    [SerializeField] private Animator zAssemblyAnimation;


    void Start()
    {
        //StartCoroutine(SpawnEnemy());
        

    }

    public void startSpawningExternal()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        if (enemyCount >= maxEnemies)
        {
            extruderTimeline.Pause();
            zAssemblyAnimation.SetTrigger("Stop");
            
            StopCoroutine(SpawnEnemy());

        }
        else
        {
            extruderTimeline.Play();
            
            zAssemblyAnimation.SetTrigger("Animate");
            zAssemblyAnimation.speed = 1f / secondsBetweenSpawns;
            zAssemblyAnimation.SetTrigger("Stop");

        }

        yield return new WaitForSeconds(secondsBetweenSpawns);
        if (enemyCount < maxEnemies)
        {
            enemyCount++;
            Instantiate(enemy, transform.position, Quaternion.identity, FindObjectOfType<EnemySpawner>().transform);
            StartCoroutine(SpawnEnemy());
            
        }
        
    }

}
