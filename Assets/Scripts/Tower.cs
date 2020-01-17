using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AimAtEnemy();
    }

    private void AimAtEnemy()
    {
        Vector3 targetXZ = new Vector3(targetEnemy.position.x, objectToPan.position.y, targetEnemy.position.z);
        objectToPan.LookAt(targetXZ);
    }
}
