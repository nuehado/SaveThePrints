using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    [SerializeField] private int currentDefenseSupportHealth = 2;
    private int initialSupportHealth;
    private EnemySpawner enemySpawner;
    private float initialEnemyMovementSpeed;
    private float enemySpawnSpeed;
    private float enemySlowTimer = 0f;
    private float effectTimer = 0f;
    [SerializeField] private float effectDuration = 3f;
    public bool isNew = true;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        if (isNew == false)
        {
            effectTimer += Time.deltaTime;
        }

        if (effectTimer >= effectDuration)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            effectTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyCollision>() != null)
        {
            isNew = false;
            StartCoroutine(EnemySlow(other.gameObject));
            /*currentDefenseSupportHealth -= 1;
            if (currentDefenseSupportHealth <= 0)
            {
                gameObject.GetComponent<DefenseSupportMover>().ResetSupportToStart();
                currentDefenseSupportHealth = initialSupportHealth;
            }*/
        }
    }

    private IEnumerator EnemySlow(GameObject enemy)
    {
        //private float slowStartTime = //needs a way to track time per benchy
        initialEnemyMovementSpeed = enemySpawner.moveSpeed;
        enemySpawnSpeed = enemySpawner.secondsBetweenSpawns;
        enemy.GetComponent<EnemyMovement>().movementSpeed = initialEnemyMovementSpeed * 0.5f;
        yield return new WaitForSeconds(enemySpawnSpeed);
        if (enemy != null)
        {
            enemy.GetComponent<EnemyMovement>().movementSpeed = initialEnemyMovementSpeed;
        }
    }
}
