﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10;
    [SerializeField] float rotateSpeed = 0.03f;
    
    List<Waypoint> path;
    private int destinationWaypoint = 0;
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
        Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Vector3.Distance(currentPosition, path[destinationWaypoint].transform.position) < movementCompleteDistance)
        {
            UpdateMovementTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, path[destinationWaypoint].transform.position, movementSpeed * Time.deltaTime);
        Vector3 directionToWaypoint = path[destinationWaypoint].transform.position - transform.position;
        Vector3 rotationStepTowardsWaypoint = Vector3.RotateTowards(transform.forward, directionToWaypoint, rotateSpeed, 0f);
        transform.rotation = Quaternion.LookRotation(rotationStepTowardsWaypoint);
    }

    private void UpdateMovementTarget()
    {
        if (path.Count == 0)
        {
            return; 
        }
        if (destinationWaypoint >= path.Count - 1)
        {
            isMoving = false;
            return;
        }

        transform.position = path[destinationWaypoint].transform.position;
        destinationWaypoint += 1;
    }

    
}