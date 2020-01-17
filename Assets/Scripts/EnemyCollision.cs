using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
   [SerializeField] int hitPoints = 1;
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        ;
        Debug.Log("HIT. hits left = " + hitPoints);
    }

    private void ProcessHit()
    {
        hitPoints = hitPoints - 1;
        
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
