using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,120f)] public float secondsBetweenSpawns = 1f;
    private int enemyCount = 0;
    [SerializeField] private int maxEnemies = 3;
    [SerializeField] private GameObject enemy;
    [SerializeField] private PlayableDirector extruderTimeline;
    [SerializeField] private Animator zAssemblyAnimation;
    private bool isPaused;
    private bool animationsPaused = false;
    private bool animationsActive = false;


    public void startSpawningExternal()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        isPaused = FindObjectOfType<PauseGame>().isPaused;
        if (animationsPaused == true && isPaused == false && animationsActive == true)
        {
            PlayPrintingAnimations();
            animationsPaused = false;
        }
        if (isPaused == true)
        {
            StopPrintingAnimations();
            animationsPaused = true;
        }
        
    }
    private IEnumerator SpawnEnemy()
    {
        animationsActive = true;
        PlayPrintingAnimations();

        enemyCount++;
        Instantiate(enemy, transform.position, Quaternion.identity, FindObjectOfType<EnemySpawner>().transform);

        yield return new WaitForSeconds(secondsBetweenSpawns);
        
        if (enemyCount < maxEnemies)
        {
            StartCoroutine(SpawnEnemy());
        }
        else
        {
            StopPrintingAnimations();
            animationsActive = false;
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
}
