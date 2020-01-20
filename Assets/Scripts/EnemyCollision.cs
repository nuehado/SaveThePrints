using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] int hitPoints = 1;
    private bool isBeingHit = false;

    ParticleSystem[] childedParticles;

    private float hitStoppedTimer = 0f;
    [SerializeField] private float hittingStoppedTime = 0.1f;

    private void Start()
    {
        childedParticles = GetComponentsInChildren<ParticleSystem>();
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
    private void StopHitParticles()
    {
        foreach (ParticleSystem hitParticle in childedParticles)
        {
            hitParticle.Stop();
        }
    }

    private void PlayHitParticles()
    {
        foreach (ParticleSystem hitParticle in childedParticles)
        {
            if (hitParticle.isStopped)
            {
                hitParticle.Play();
            }

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
