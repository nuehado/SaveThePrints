using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridUnitSize = 10;
    // okay to be public as is a data class
    public bool isExploreOff = false;
    public bool isPlacable = true; // todo I think these three bools may have some redundency, refactor
    public bool isBlocked = false;
    public Waypoint exploredFrom;

    public int GetGridSize()
    {
        return gridUnitSize;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int
        (
            Mathf.RoundToInt(transform.position.x / gridUnitSize),
            Mathf.RoundToInt(transform.position.z / gridUnitSize)
        );
    }


    /*private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isPlacable == true)
        {
            FindObjectOfType<TowerSpawnController>().AddTower(this);
        }
    }*/
}
