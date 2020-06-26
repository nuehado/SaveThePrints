using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode1 : MonoBehaviour
{

    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float radius;
    private float destroyLifetime;
    [SerializeField] private float maxLifetime = 2f;
    [SerializeField] private Vector3 explodePointRandomizer;


    void OnEnable()
    {
        ExplodeMesh();
        destroyLifetime = 0f;
    }

    private void Update()
    {
        destroyLifetime += Time.deltaTime;
        if(destroyLifetime >= maxLifetime)
        {
            Destroy(this.gameObject);
        }
    }

    public void ExplodeMesh()
    {
        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if (rb != null)
            {
                explodePointRandomizer = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position + explodePointRandomizer, radius);
            }
        }
    }
}
