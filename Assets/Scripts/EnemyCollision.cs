using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] int hitPoints = 1;
    private bool isBeingHit = false;

    [SerializeField] ParticleSystem[] hitParticles;
    [SerializeField] ParticleSystem deathExplosion;
    [SerializeField] AudioClip deathSound;
    private EnemySpawner enemySpawner;

    private float hitStoppedTimer = 0f;
    [SerializeField] private float hittingStoppedTime = 0.1f;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    void Update()
    {
        if (hitPoints <= 0)
        {
            KillEnemy();
            enemySpawner.UpdateEnemyHealth(-1);
        }

        CheckForStoppedHitting();

        if (isBeingHit == true)
        {
            PlayHitParticles();
        }
            
        else if (isBeingHit == false)
        {
            StopHitParticles();
        }  
    }

    private void KillEnemy()
    {
        var explodeVFX = Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Destroy(explodeVFX.gameObject, 1f);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        Destroy(gameObject);
    }

    private void CheckForStoppedHitting()
    {
        hitStoppedTimer = hitStoppedTimer + Time.deltaTime;
        if (hitStoppedTimer > hittingStoppedTime)
        {
            isBeingHit = false;
        }
    }
    private void PlayHitParticles()
    {
        foreach (ParticleSystem hitParticle in hitParticles)
        {
            if (hitParticle.isStopped)
            {
                hitParticle.Play();
            }
        }
    }

    private void StopHitParticles()
    {
        foreach (ParticleSystem hitParticle in hitParticles)
        {
            hitParticle.Stop();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        isBeingHit = true;
        hitStoppedTimer = 0f;
        ProcessHit();
    }

    private void ProcessHit()
    {
        hitPoints = hitPoints - 1;
    }
}
