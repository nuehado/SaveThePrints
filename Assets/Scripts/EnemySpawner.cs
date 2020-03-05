using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,120f)] public float secondsBetweenSpawns = 1f;
    private int enemyCount = 0;
    private int enemyHealth;
    [SerializeField] private int maxEnemies = 3;
    [SerializeField] private GameObject enemy;
    [SerializeField] private PlayableDirector extruderTimeline;
    [SerializeField] private Animator zAssemblyAnimation;
    private LoadManager loadManager;

    private void Start()
    {
        loadManager = FindObjectOfType<LoadManager>();
        enemyHealth = maxEnemies;
    }

    public void UpdateEnemyHealth(int enemyAddORSub)
    {
        enemyHealth += enemyAddORSub;
        if (enemyHealth <= 0)
        {
            Debug.Log("You Win this level!");
            loadManager.WinLevel();
        }
    }

    public void startSpawningExternal()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {

        PlayPrintingAnimations();

        enemyCount++;
        Instantiate(enemy, transform.position, Quaternion.identity, FindObjectOfType<EnemySpawner>().transform);

        yield return new WaitForSeconds(secondsBetweenSpawns);
        
        if (enemyCount < maxEnemies )
        {
            StartCoroutine(SpawnEnemy());
        }
        else
        {
            StopPrintingAnimations();
        }
    }

    private void StopPrintingAnimations()
    {
        extruderTimeline.Pause();
        zAssemblyAnimation.SetTrigger("Stop");
    }

    private void PlayPrintingAnimations()
    {
        extruderTimeline.Play();
        zAssemblyAnimation.SetTrigger("Animate");
        zAssemblyAnimation.speed = 1f / secondsBetweenSpawns;
        zAssemblyAnimation.SetTrigger("Stop");
    }

    public void ClearEnemies()
    {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        enemyCount = 0;
        StopAllCoroutines();
        extruderTimeline.Stop();
        zAssemblyAnimation.SetTrigger("Stop");
    }
}
