using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] int enemyHitPoints = 1;
    private bool isBeingHit = false;

    [SerializeField] ParticleSystem[] hitParticles;
    [SerializeField] GameObject deathObjects;
    [SerializeField] ParticleSystem deathExplosion;
    [SerializeField] AudioClip deathSound;
    private EnemySpawner enemySpawner;

    private float hitStoppedTimer = 0f;
    [SerializeField] private float hittingStoppedTime = 0.95f;

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
        var explodeVFX = Instantiate(deathExplosion, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), Quaternion.identity);
        //var deathExplode = Instantiate(deathObjects, transform.position, transform.rotation);
        var deathExplode = FragmentPool.Instance.Get();
        deathExplode.transform.position = transform.position;
        deathExplode.transform.rotation = transform.rotation;
        deathExplode.SetActive(true);
        Destroy(explodeVFX.gameObject, 1f);
        //Destroy(deathExplode.gameObject, 1.5f);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, 0.2f);
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

    public void ManualDamage(int damage)
    {
        enemyHitPoints -= damage;
    }
}
