using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float radius;
    private float destroyLifetime;
    [SerializeField] private float maxLifetime = 2f;
    [SerializeField] private Vector3 explodePointRandomizer;
    private List<Vector3> originalFragmentPositions = new List<Vector3>();
    private List<Transform> fragmentTransforms = new List<Transform>();


    private void Start()
    {
        foreach (Transform transform in transform)
        {
            fragmentTransforms.Add(transform);
            originalFragmentPositions.Add(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z));
        }
    }

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
            FragmentPool.Instance.ReturnToPool(this.gameObject);
            for(int i = 0; i < fragmentTransforms.Count; i++)
            {
                fragmentTransforms[i].localPosition = originalFragmentPositions[i];
            }
            destroyLifetime = 0f;
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
