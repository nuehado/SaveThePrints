﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    Queue<Waypoint> waypointQueue = new Queue<Waypoint>();

    [SerializeField] public Waypoint startWaypoint;
    [SerializeField] public Waypoint endWaypoint;

    private Waypoint searchCenter;

    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    bool isRunning = true;

    private List<Waypoint> path = new List<Waypoint>();



    public List<Waypoint> GetPath()
    {
        BuildBlockDictionary();
        ColorStartAndEnd();
        BreadthFirstSearchForPath();
        FormPath();
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
                Debug.Log("skipping duplicate block: " + waypoint.name);
            }
            else
            {
                grid.Add(waypoint.GetGridPosition(), waypoint);
                
            }
            
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.white);
        endWaypoint.SetTopColor(Color.red);
    }

    private void BreadthFirstSearchForPath()
    {

        waypointQueue.Enqueue(startWaypoint);


        while (waypointQueue.Count > 0 && isRunning == true)
        {
            searchCenter = waypointQueue.Dequeue();
            Debug.Log("searching from: " + searchCenter); // todo remove log
            HaltIfEndFound(searchCenter);
            ExploreNeighbors(searchCenter);
            searchCenter.isExplored = true;
        }

        Debug.Log("Finished Pathfind()");
    }

    private void HaltIfEndFound(Waypoint start)
    {
        if (start == endWaypoint)
        {
            Debug.Log("start and end are the same. stopping..."); // todo remove log
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
        if (neighbor.isExplored == false)
        {
            
            waypointQueue.Enqueue(neighbor);
            neighbor.isExplored = true; // todo I added this because It removes duplicates faster, I'm not sure if it breaks the breadth first search algorithm somehow though
            neighbor.exploredFrom = searchCenter;
            Debug.Log("Queueing neighbor: " + neighbor);
        }
    }

    private void FormPath()
    {
        path.Add(endWaypoint);
        Waypoint previousWaypoint = endWaypoint.exploredFrom;
        while (previousWaypoint != startWaypoint)
        {
            //add intermediate waypoints
            path.Add(previousWaypoint);
            previousWaypoint = previousWaypoint.exploredFrom;

        }
        path.Add(startWaypoint);
        path.Reverse();
    }
}
