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
    private EnemyMovement[] enemies;


    private void Start()
    {
        pauseGame = GameObject.FindObjectOfType<PauseGame>();
    }
    private void Update()
    {
        if (playerHealth <= 0)
        {
            enemies = GameObject.FindObjectsOfType<EnemyMovement>();
            foreach (EnemyMovement enemy in enemies)
            {
                Destroy(enemy.gameObject);
                Debug.Log(enemy + "destroyed");
            }
            printingMenu.gameObject.SetActive(false);
            LoseMenu.gameObject.SetActive(true);
            printLoader.ChangeLevel(0);
            
            Destroy(gameObject);
            pauseGame.PausePlayButton();
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
