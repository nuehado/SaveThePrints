﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMover : MonoBehaviour
{
    private GameObject drag;
    private Vector3 updatePosition;
    private float planeY = -0.75f;
    private Plane plane;
    private Ray ray;
    private GameObject gameObjectBelow = null;
    private Vector3 newTowerPosition;

    private LineRenderer checkLineRenderer;


    private void Start()
    {
        plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane
        newTowerPosition = (gameObject.transform.position);
        checkLineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (gameObject == hit.transform.gameObject)
                {
                    drag = hit.transform.gameObject;
                }
                
            }
            float distance; // the distance from the ray origin to the ray intersection of the plane
            
            if (drag != null)
            {
                if (plane.Raycast(ray, out distance))
                {
                    updatePosition = ray.GetPoint(distance);
                    drag.transform.position = new Vector3(updatePosition.x, planeY + 20f, updatePosition.z); // distance along the ray
                }
            }
                    
            
        }
        
        if (drag != null)
        {
            RaycastHit hit;
            Ray placementCheckRay = new Ray(transform.position, -transform.up);
            if (Physics.Raycast(placementCheckRay, out hit, 100.0f))
            {
                if (hit.transform.gameObject.GetComponent("Waypoint") != null)
                {
                    gameObjectBelow = hit.transform.gameObject;
                    newTowerPosition = new Vector3(gameObjectBelow.transform.position.x, planeY + 4f, gameObjectBelow.transform.position.z);
                    checkLineRenderer.enabled = true;
                    checkLineRenderer.SetPosition(0, transform.position);
                    checkLineRenderer.SetPosition(1, new Vector3(transform.position.x, planeY, transform.position.z));
                }
                else
                {
                    checkLineRenderer.enabled = false;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            drag.transform.position = newTowerPosition;
            drag = null;
        }

        if (drag == null)
        {
            checkLineRenderer.enabled = false;
        }
    }
}
