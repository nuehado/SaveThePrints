using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy; // todo need to change to update to closes enemy
    [SerializeField] float AttackRange = 20f;
    [SerializeField] ParticleSystem projectileParticles;
    private float fireDelay;
    [SerializeField] private float startupDelay = 0.1f;
    [SerializeField] private float stopDelay = 0f;

    private bool isInRange = false;

    private Vector3 enemyPosition;


    void Start()
    {
        enemyPosition = targetEnemy.position;
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        
        if(targetEnemy)
        {
            AimAtEnemy();
        }
        else
        {
            Fire(false);
        }
        
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyCollision>();
        if (sceneEnemies.Length == 0)
        {
            return;
        }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyCollision testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;

    }

    private Transform GetClosestEnemy(Transform currentEnemy, Transform testEnemy)
    {
        float testEnemyDistance = Mathf.Abs(Vector3.Distance(testEnemy.position, objectToPan.position));
        float currentEnemyDistance = Mathf.Abs(Vector3.Distance(currentEnemy.position, objectToPan.position));

        if (testEnemyDistance < currentEnemyDistance)
        {
            currentEnemy = testEnemy;
        }

        return currentEnemy;
    }

    private void AimAtEnemy()
    {
        if (targetEnemy == null)
        {
            return;
        }
        else
        {
            CheckEnemyRange();
            Vector3 targetXZ = new Vector3(targetEnemy.transform.position.x, objectToPan.position.y, targetEnemy.transform.position.z);
            objectToPan.LookAt(targetXZ);
        }

    }

    private void CheckEnemyRange()
    {
        float enemyDistance = Mathf.Abs(Vector3.Distance(targetEnemy.position, objectToPan.position));
        

        if (enemyDistance <= AttackRange)
        {
            isInRange = true;
            fireDelay = startupDelay;
        }
        else
        {
            isInRange = false;
            fireDelay = stopDelay;
        }

        Fire(isInRange);
    }
    
    private void Fire(bool isInRage)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isInRage;
    }
}

