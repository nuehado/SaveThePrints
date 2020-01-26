using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth = 3;
    [SerializeField] Text healthtext;
    [SerializeField] ParticleSystem goalParticles;

    private void Update()
    {
        healthtext.text = "The Child Health: " + playerHealth.ToString();
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
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
