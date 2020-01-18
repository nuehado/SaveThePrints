using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridUnitSize = 10;
    // okay to be public as is a data class
    public bool isExploreOff = false;
    public bool isPlacable = true; // todo set based on presence of blocker object or tower
    public Waypoint exploredFrom;
    [SerializeField] private GameObject towerPrefab;


    private void Start()
    {
        if (isPlacable == false) // todo add this to the pathfinder script instead of on start. 
        {
            isExploreOff = true;
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


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Waypoint>().isPlacable == true)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity, FindObjectOfType<Towers>().transform);
            isPlacable = false;
        }
    }
}
