using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy; 
    [SerializeField] float AttackRange = 20f;
    [SerializeField] ParticleSystem projectileParticles;
    private float fireDelay;
    [SerializeField] private float startupDelay = 0.1f;
    [SerializeField] private float stopDelay = 0f;

    private bool isInRange = false;

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
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;

    }

    private Transform GetClosest(Transform transformA, Transform TransformB)
    {
        float testEnemyDistance = Mathf.Abs(Vector3.Distance(TransformB.position, transform.position));
        float currentEnemyDistance = Mathf.Abs(Vector3.Distance(transformA.position, transform.position));

        if (testEnemyDistance < currentEnemyDistance)
        {
            transformA = TransformB;
        }

        return transformA;
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

