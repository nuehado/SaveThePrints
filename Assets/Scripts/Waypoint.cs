using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridUnitSize = 10;
    // okay to be public as is a data class
    public bool isExploreOff = false;
    public Waypoint exploredFrom;

    private void Update()
    {
        SetExplorationColor(); // todo remove this once Pathfinder is done or add as debug only option
    }

    private void SetExplorationColor()
    {
        if (isExploreOff == true)
        {
            SetTopColor(Color.blue);
        }
        else
        {
            SetTopColor(Color.white);
        }
    }

    public int GetGridSize()
    {
        return gridUnitSize;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridUnitSize),
            Mathf.RoundToInt(transform.position.z / gridUnitSize)
        );
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    private void OnMouseOver()
    {
        Debug.Log("mouse over: " + gameObject.name);
    }
}
