using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSupportHealth : MonoBehaviour
{
    private int currentDefenseSupportHealth = 4;
    [SerializeField] GameObject deathObjects;
    [SerializeField] private int initialSupportHealth = 4;
    private GameObject previousObject = null;

    private void Start()
    {
        currentDefenseSupportHealth = initialSupportHealth;
    }

    private void OnEnable()
    {
        currentDefenseSupportHealth = initialSupportHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyCollision>() != null)
        {
            if( other.gameObject != previousObject)
            {
                previousObject = other.gameObject;
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
    }

    private void EnemySelfDestruct(GameObject enemy)
    {
        enemy.GetComponent<EnemyCollision>().KillEnemy(false);
    }
}
