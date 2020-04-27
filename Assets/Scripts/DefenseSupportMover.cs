using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSupportMover : MonoBehaviour
{
    private GameObject drag;
    private Vector3 updatePosition;
    private float planeY = -0.75f;
    private Plane plane;
    private Ray ray;
    private Vector3 newSupportPosition;
    private Vector3 oldSupportPosition;
    public Vector3 initialSupportPosition;

    private LineRenderer checkLineRenderer;
    private Waypoint waypointSupportIsOver;
    private Waypoint previousPlacementWaypoint = null;
    private Waypoint validPlacementWaypoint;
    private bool isSupportPlaced = false;

    private void Start()
    {
        plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane
        initialSupportPosition = (gameObject.transform.position);
        oldSupportPosition = initialSupportPosition;
        checkLineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectSupport();
        }

        if (Input.GetMouseButton(0) && drag != null)
        {
            DragSupport();
        }

        if (Input.GetMouseButtonUp(0) && drag != null)
        {
            PlaceSupportInNewPosition();
        }
            
        if (drag == null)
        {
            checkLineRenderer.enabled = false;
        }
    }

    private void SelectSupport()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500.0f))
        {
            if (gameObject == hit.transform.gameObject && isSupportPlaced == false)
            {
                drag = hit.transform.gameObject;
            }
        }
    }

    private void DragSupport()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500.0f))
        {

            float distance; // the distance from the ray origin to the ray intersection of the plane

            if (plane.Raycast(ray, out distance))
            {
                updatePosition = ray.GetPoint(distance);
                drag.transform.position = new Vector3(updatePosition.x, planeY + 15f, updatePosition.z); // distance along the ray
            }

            CheckForPlacementAllowed();

        }
    }

    private void CheckForPlacementAllowed()
    {
        RaycastHit hit;
        Ray placementCheckRay = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(placementCheckRay, out hit, 100.0f))
        {
            if (hit.transform.gameObject.TryGetComponent(typeof(Waypoint), out Component waypointOver))
            {
                waypointSupportIsOver = waypointOver.GetComponent<Waypoint>();

                if (previousPlacementWaypoint != null)
                {
                    previousPlacementWaypoint.isOnPath = true;
                }

                if (waypointSupportIsOver.isOnPath == true)
                {
                    validPlacementWaypoint = waypointSupportIsOver;
                    checkLineRenderer.enabled = true;
                    checkLineRenderer.SetPosition(0, new Vector3(validPlacementWaypoint.transform.position.x, planeY, validPlacementWaypoint.transform.position.z));
                    checkLineRenderer.SetPosition(1, new Vector3(validPlacementWaypoint.transform.position.x, planeY + 5f, validPlacementWaypoint.transform.position.z));
                }
            }
        }
    }

    private void PlaceSupportInNewPosition()
    {
        if (validPlacementWaypoint == null)
        {
            drag.transform.position = oldSupportPosition;
        }
        else
        {
            newSupportPosition = new Vector3(validPlacementWaypoint.transform.position.x, planeY, validPlacementWaypoint.transform.position.z);
            drag.transform.position = newSupportPosition;
            validPlacementWaypoint.isOnPath = false;
            isSupportPlaced = true;
            previousPlacementWaypoint = validPlacementWaypoint;
        }
        drag = null;
    }

    public void ResetSupportToStart()
    {
        gameObject.transform.position = initialSupportPosition;
        validPlacementWaypoint = null;
        oldSupportPosition = initialSupportPosition;
        isSupportPlaced = false;
        if (previousPlacementWaypoint != null)
        {
            previousPlacementWaypoint.isOnPath = true; //todo check for no support placed
        }
        gameObject.SetActive(false);
    }
}
