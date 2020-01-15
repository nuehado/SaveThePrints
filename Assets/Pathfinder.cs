using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] public Waypoint startWaypoint;
    [SerializeField] public Waypoint endWaypoint;

    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    // Start is called before the first frame update
    void Start()
    {
        BuildBlockDictionary();
        ColorStartAndEnd();
        ExploreNeighbors();
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

    private void ExploreNeighbors()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoordinates = startWaypoint.GetGridPosition() + direction;
            try
            {
                grid[explorationCoordinates].SetTopColor(Color.blue);
            }
            catch
            {
                //negatory
            }
        }

    }
}
