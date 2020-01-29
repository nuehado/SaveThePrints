using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class EditorSnap : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
        UpdateBlocker();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        Vector2 gridPosition = waypoint.GetGridPosition();
        transform.position = new Vector3(gridPosition.x * gridSize, 0.0f, gridPosition.y * gridSize);
    }

    private void UpdateLabel()
    {
        int gridSize = waypoint.GetGridSize();
        Vector2 gridPosition = waypoint.GetGridPosition();
        TextMesh coordinateLabel = GetComponentInChildren<TextMesh>();
        string textLabel = gridPosition.x + "," + gridPosition.y;
        coordinateLabel.text = textLabel;
        gameObject.name = "cube " + textLabel;
    }

    private void UpdateBlocker()
    {
        var blockerObject = transform.Find("Supports");
        if (waypoint.isBlocked == true)
        {
            blockerObject.gameObject.SetActive(true);
        }
        else
        {
            blockerObject.gameObject.SetActive(false);
        }
    }

}
