using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFiring : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy; 
    [SerializeField] float AttackRange = 20f;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] private float rotateSpeed = 0.1f;

    [SerializeField] [Range(0.0f, 1.2f)] private float towerRotateSFXPitch;

    private bool isInRange = false;
    private bool isSFXPlaying = false;
    private float angletoTarget;
    private float pitchChangePerDegree = 0.5f / 360f;
    AudioSource rotatingSFX;
    AudioSource shootingSFX;

    public Waypoint waypointTowerIsOn;
    private bool isFiringOn = false;


    private void Start()
    {
        var audioSources = GetComponents<AudioSource>();
        rotatingSFX = audioSources[0];
        shootingSFX = audioSources[1];
    }

    void Update()
    {
        
        if (isFiringOn == true)
        {
            SetTargetEnemy();
        }

        if (targetEnemy == true && isFiringOn == true)
        {
            AimAtEnemy();
        }
        else
        {
            Fire(false);
        }

        //PlayRotationSFX();
        
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

            Vector3 directionToTarget = targetXZ - objectToPan.transform.position;
            angletoTarget = Vector3.Angle(directionToTarget, objectToPan.transform.forward);
            Vector3 stepTowardsTarget = Vector3.RotateTowards(objectToPan.transform.forward, directionToTarget, rotateSpeed, 0f);
            objectToPan.transform.rotation = Quaternion.LookRotation(stepTowardsTarget);
        }
    }

    private void CheckEnemyRange()
    {
        float enemyDistance = Mathf.Abs(Vector3.Distance(targetEnemy.position, objectToPan.position));

        if (enemyDistance <= AttackRange)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }

        Fire(isInRange);
    }
    
    private void Fire(bool isInRage)
    {
        var emissionModule = projectileParticles.emission;

        emissionModule.enabled = isInRage;

        PlayFireSFX(isInRage);

    }

    private void PlayRotationSFX()
    {
        towerRotateSFXPitch = angletoTarget * pitchChangePerDegree;
        if (isInRange == false)
        {
            rotatingSFX.volume = 0.2f;
        }
        if (isInRange == true)
        {
            rotatingSFX.volume = 0.5f;
        }
        rotatingSFX.pitch = 0.4f + towerRotateSFXPitch;
    }
    private void PlayFireSFX(bool isInRage)
    {
        if (isInRage && isSFXPlaying == false)
        {
            shootingSFX.Play();

            isSFXPlaying = true;
        }
        else if (isInRage == false)
        {
            shootingSFX.Stop();
            isSFXPlaying = false;
        }
    }

    public void SetTargeting(bool shouldTarget)
    {
        isFiringOn = shouldTarget;
    }
}

