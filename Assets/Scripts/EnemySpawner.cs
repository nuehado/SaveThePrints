using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,120f)] public float secondsBetweenSpawns = 1f;
    public float moveSpeed = 10f;
    public int enemyCount = 0;
    public int enemyHealth;
    public int maxEnemies = 3;
    [SerializeField] private GameObject enemy;
    [SerializeField] private PlayableDirector extruderTimeline;
    [SerializeField] private Animator zAssemblyAnimation;
    private LoadManager loadManager;
    PlayerHealth playerHealth;

    [SerializeField] private Slider sliderPSpeed;
    [SerializeField] private Slider sliderMSpeed;

    private void Start()
    {
        loadManager = FindObjectOfType<LoadManager>();
        enemyHealth = maxEnemies;
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
        enemyHealth = maxEnemies;
        StopAllCoroutines();
        extruderTimeline.Stop();
        zAssemblyAnimation.SetTrigger("Stop");
    }

    public void UpdateEnemyHealth(int enemyAddORSub)
    {
        enemyHealth += enemyAddORSub; // todo this is called whenever enemyHealth = 0, we want to check if player health is also > 0 inside loadManager. If it is then we win
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (enemyHealth <= 0 && playerHealth.playerHealth > 0)
        {
            Debug.Log("You Win this level!");
            loadManager.WinLevel();
        }
    }

    public void ChangePrintSpeed()
    {
        secondsBetweenSpawns = sliderPSpeed.value;
    }

    public void ChangeMoveSpeed()
    {
        moveSpeed = sliderMSpeed.value;
    }
}
