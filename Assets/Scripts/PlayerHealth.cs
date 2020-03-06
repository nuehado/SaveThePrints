using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 3;
    public int maxPlayerHealth = 3;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] ParticleSystem goalParticles;
    [SerializeField] LoadManager loadManager;

    private void OnEnable()
    {
        healthText.text = "Target Integrity   " + playerHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyCollision>() != null)
        {
            playerHealth = playerHealth - 1;
            EnemySelfDestruct(other.gameObject);
            healthText.text = "Target Integrity   " + playerHealth.ToString();
            if (playerHealth <= 0)
            {
                loadManager.LoseLevel();
            }
        }
    }

    private void EnemySelfDestruct(GameObject enemy)
    {
        var goalVFX = Instantiate(goalParticles, enemy.transform.position, Quaternion.identity);
        Destroy(goalVFX.gameObject, 0.5f);
        enemy.GetComponent<EnemyCollision>().KillEnemy(false);
        //Destroy(enemy);
    }
}
