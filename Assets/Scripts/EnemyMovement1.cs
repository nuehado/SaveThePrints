using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10;
    [SerializeField] float rotateSpeed = 0.03f;
    [SerializeField] ParticleSystem goalParticles;

    List<Waypoint> path;

    private int destinationPoint = 0;
    private float movementCompleteDistance = 0.1f;
    private bool isMoving = true;

    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.GetPath();
        UpdateMovementTarget();

    }

    private void Update()
    {
        if (isMoving == true)
        {
            MoveEnemy();
        }

    }

    private void MoveEnemy()
    {
        Vector3 thisPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (Vector3.Distance(thisPosition, path[destinationPoint].transform.position) < movementCompleteDistance)
        {
            UpdateMovementTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, path[destinationPoint].transform.position, movementSpeed * Time.deltaTime);

        Vector3 directionToWaypoint = path[destinationPoint].transform.position - transform.position;
        Vector3 stepTowardsWaypoint = Vector3.RotateTowards(transform.forward, directionToWaypoint, rotateSpeed, 0f);
        transform.rotation = Quaternion.LookRotation(stepTowardsWaypoint);

        
    }

    private void UpdateMovementTarget()
    {
        if (path.Count == 0)
        {
            
            return; 

        }
        if (destinationPoint >= path.Count - 1)
        {
            SelfDestruct();
            isMoving = false;
            return;
        }

        transform.position = path[destinationPoint].transform.position;
        destinationPoint += 1;
        
    }

    private void SelfDestruct()
    {
        var goalVFX = Instantiate(goalParticles, transform.position, Quaternion.identity);
        Destroy(goalVFX.gameObject, 0.5f);
        Destroy(gameObject);
    }
}