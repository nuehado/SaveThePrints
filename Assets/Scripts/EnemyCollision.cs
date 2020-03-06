using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] int enemyHitPoints = 1;
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
        if (enemyHitPoints <= 0)
        {
            KillEnemy(true);
            
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

    public void KillEnemy(bool isKilled)
    {
        var explodeVFX = Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Destroy(explodeVFX.gameObject, 1f);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        enemySpawner.UpdateEnemyHealth(-1);
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
        enemyHitPoints = enemyHitPoints - 1;
    }
}
