using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float radius;
    [SerializeField] private float destroyDelay;
    [SerializeField] private Vector3 explodePointRandomizer;

    // Start is called before the first frame update
    void OnEnable()
    {
        ExplodeMesh();
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

            Destroy(t.gameObject, destroyDelay);
        }
    }
}
