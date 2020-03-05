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

    private void Update()
    {
        if (playerHealth <= 0)
        {
            loadManager.LoseLevel();
        }
    }
    private void OnEnable()
    {
        healthText.text = "Target Integrity   " + playerHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerHealth = playerHealth - 1;
        EnemySelfDestruct(other.gameObject);
        healthText.text = "Target Integrity   " + playerHealth.ToString();
    }

    private void EnemySelfDestruct(GameObject enemy)
    {
        var goalVFX = Instantiate(goalParticles, enemy.transform.position, Quaternion.identity);
        Destroy(goalVFX.gameObject, 0.5f);
        Destroy(enemy);
    }
}
