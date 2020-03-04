using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth = 3;
    [SerializeField] Text healthtext;
    [SerializeField] ParticleSystem goalParticles;
    [SerializeField] Canvas printingMenu;
    [SerializeField] Canvas LoseMenu;
    [SerializeField] PrintLoader printLoader;
    private PauseGame pauseGame;
    private EnemySpawner enemySpawner;
    private TowerMover[] towers;
    [SerializeField] private GameObject currentLevel;


    private void Start()
    {
        pauseGame = GameObject.FindObjectOfType<PauseGame>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        towers = FindObjectsOfType<TowerMover>();
    }
    private void Update()
    {
        if (playerHealth <= 0)
        {
            printingMenu.gameObject.SetActive(false);
            LoseMenu.gameObject.SetActive(true);
            printLoader.ChangeLevel(0);
            enemySpawner.ClearEnemies();
            //pauseGame.PausePlayButton();
            currentLevel.SetActive(false);
            playerHealth = 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerHealth = playerHealth - 1;
        EnemySelfDestruct(other.gameObject);
    }

    private void EnemySelfDestruct(GameObject enemy)
    {
        var goalVFX = Instantiate(goalParticles, enemy.transform.position, Quaternion.identity);
        Destroy(goalVFX.gameObject, 0.5f);
        Destroy(enemy);
    }
}
