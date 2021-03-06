﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    Queue<Waypoint> waypointQueue = new Queue<Waypoint>();

    [SerializeField] public Waypoint startWaypoint;
    [SerializeField] public Waypoint endWaypoint;

    private Waypoint searchCenter;

    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    bool isRunning = true;

    private List<Waypoint> path = new List<Waypoint>();



    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            BuildBlockDictionary();
            BreadthFirstSearchForPath();
            FormPath();
        }

        return path;
    }

    private void BuildBlockDictionary()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPosition());
            if (isOverlapping == true)
            {
                //empty
            }
            else if (waypoint.isBlocked == true)
            {
                waypoint.isPlacable = false;
                waypoint.isExploreOff = true;
            }
            else
            {
                grid.Add(waypoint.GetGridPosition(), waypoint);
            }
        }
    }

    

    private void BreadthFirstSearchForPath()
    {
        waypointQueue.Enqueue(startWaypoint);

        while (waypointQueue.Count > 0 && isRunning == true)
        {
            searchCenter = waypointQueue.Dequeue();
            HaltIfEndFound(searchCenter);
            ExploreNeighbors(searchCenter);
            searchCenter.isExploreOff = true;
        }
    }

    private void HaltIfEndFound(Waypoint start)
    {
        if (start == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbors(Waypoint searchCenter)
    {
        if (isRunning == false)
        {
            return;
        }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = searchCenter.GetGridPosition() + direction;
            
            if (grid.ContainsKey(neighborCoordinates))
            {
                Waypoint neighbor = grid[neighborCoordinates];
                QueueNewNeighbor(neighbor);
            }
        }
    }

    private void QueueNewNeighbor(Waypoint neighbor)
    {
        if (neighbor.isExploreOff == false)
        {
            waypointQueue.Enqueue(neighbor);
            neighbor.isExploreOff = true; 
            neighbor.exploredFrom = searchCenter;
        }
    }

    private void FormPath()
    {
        SetAsPath(endWaypoint);
        Waypoint previousWaypoint = endWaypoint.exploredFrom;
        while (previousWaypoint != startWaypoint)
        {
            //add intermediate waypoints
            SetAsPath(previousWaypoint);
            previousWaypoint.isOnPath = true;
            previousWaypoint = previousWaypoint.exploredFrom;
        }
        SetAsPath(startWaypoint);
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypointOnPath)
    {
        path.Add(waypointOnPath);
        waypointOnPath.isPlacable = false;
    }
}
