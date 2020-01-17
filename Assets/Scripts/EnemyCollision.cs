using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
   [SerializeField] int hits = 1;
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessHits();
    }

    private void OnParticleCollision(GameObject other)
    {
        hits = hits - 1;
        Debug.Log("HIT. hits left = " + hits);
    }

    private void ProcessHits()
    {
        if (hits <=0)
        {
            Destroy(gameObject);
        }
    }
}
