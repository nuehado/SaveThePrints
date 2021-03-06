﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnController : MonoBehaviour
{
    [SerializeField] private TowerFiring towerPrefab;
    [SerializeField] private int towerLimit = 3;

    //add empty queue of towers
    Queue<TowerFiring> towerQueue = new Queue<TowerFiring>();

    public void AddTower(Waypoint newWaypoint)
    {
        
        if (towerQueue.Count < towerLimit)
        {
            TowerFiring newTower = Instantiate(towerPrefab, newWaypoint.transform.position, Quaternion.identity, FindObjectOfType<Towers>().transform);
            newTower.waypointTowerIsOn = newWaypoint;
            towerQueue.Enqueue(newTower);
            newWaypoint.isPlacable = false;
        }
        
        else
        {
            MoveExistingTower(newWaypoint);
        }
    }

    private void MoveExistingTower(Waypoint newWaypoint)
    {
        TowerFiring oldestTower = towerQueue.Dequeue();
        oldestTower.waypointTowerIsOn.isPlacable = true;
        oldestTower.transform.position = newWaypoint.transform.position;
        oldestTower.waypointTowerIsOn = newWaypoint;
        newWaypoint.isPlacable = false;
        towerQueue.Enqueue(oldestTower);
    }
}
