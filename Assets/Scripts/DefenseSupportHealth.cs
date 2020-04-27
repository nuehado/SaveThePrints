using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSupportHealth : MonoBehaviour
{
    [SerializeField] private int currentDefenseSupportHealth = 2;
    [SerializeField] GameObject deathObjects;
    private int initialSupportHealth;

    private void Start()
    {
        initialSupportHealth = currentDefenseSupportHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyCollision>() != null)
        {
            EnemySelfDestruct(other.gameObject);
            currentDefenseSupportHealth -= 1;
            if (currentDefenseSupportHealth <= 0)
            {
                var deathExplode = Instantiate(deathObjects, transform.position, transform.rotation);
                Destroy(deathExplode.gameObject, 3f);
                gameObject.GetComponent<DefenseSupportMover>().ResetSupportToStart();
                currentDefenseSupportHealth = initialSupportHealth;
            }
        }
    }

    private void EnemySelfDestruct(GameObject enemy)
    {
        enemy.GetComponent<EnemyCollision>().KillEnemy(false);
    }
}
