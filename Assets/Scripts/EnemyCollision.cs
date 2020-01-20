using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] int hitPoints = 1;
    private bool isBeingHit = false;

    [SerializeField] ParticleSystem[] hitParticles;
    [SerializeField] ParticleSystem deathExplosion;

    private float hitStoppedTimer = 0f;
    [SerializeField] private float hittingStoppedTime = 0.1f;

    private void Start()
    {

    }

    void Update()
    {
        if (hitPoints <= 0)
        {
            KillEnemy();
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
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
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
        
        //Debug.Log("HIT. hits left = " + hitPoints);
    }

    private void ProcessHit()
    {
        
        
        hitPoints = hitPoints - 1;

        
    }


}
