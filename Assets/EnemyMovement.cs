using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;

    void Start()
    {
        PrintPathCoordinates();
    }

    private void PrintPathCoordinates()
    {
        foreach (Waypoint block in path)
        {
            Debug.Log(block.name);
        }
    }

    void Update()
    {
        
    }
}
