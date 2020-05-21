using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMover : MonoBehaviour
{
    [SerializeField] Transform pivotTransform;
    private GameObject drag;
    private Vector3 updatePosition;
    private float planeY = -0.75f;
    private Plane plane;
    private Ray ray;
    private Vector3 newTowerPosition;
    private Vector3 oldTowerPosition;
    private Vector3 initialTowerPosition;

    private Quaternion initialTowerRotation;

    private LineRenderer checkLineRenderer;
    private Waypoint waypointTowerIsOver;
    private Waypoint previousPlacementWaypoint = null;
    private Waypoint validPlacementWaypoint;
    private TowerFiring towerFiring;

    private void Start()
    {
        plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane
        initialTowerPosition = gameObject.transform.position;
        oldTowerPosition = initialTowerPosition;
        initialTowerRotation = pivotTransform.rotation;
        checkLineRenderer = GetComponent<LineRenderer>();
        towerFiring = GetComponent<TowerFiring>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectTower();
        }

        if (Input.GetMouseButton(0) && drag != null)
        {
            DragTower();
        }

        if (Input.GetMouseButtonUp(0) && drag != null)
        {
            PlaceTowerInNewPosition();
        }
            
        if (drag == null)
        {
            checkLineRenderer.enabled = false;
        }
    }

    private void SelectTower()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500.0f))
        {
            if (gameObject == hit.transform.gameObject)
            {
                drag = hit.transform.gameObject;
                towerFiring.SetTargeting(false); 
            }
        }
    }

    private void DragTower()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500.0f))
        {
            if (gameObject == hit.transform.gameObject)
            {
                towerFiring.SetTargeting(false);
            }

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
                waypointTowerIsOver = waypointOver.GetComponent<Waypoint>();

                if (previousPlacementWaypoint != null)
                {
                    previousPlacementWaypoint.isPlacable = true;
                }

                if (waypointTowerIsOver.isPlacable == true)
                {
                    validPlacementWaypoint = waypointTowerIsOver;
                    checkLineRenderer.enabled = true;
                    checkLineRenderer.SetPosition(0, new Vector3(validPlacementWaypoint.transform.position.x, planeY, validPlacementWaypoint.transform.position.z));
                    checkLineRenderer.SetPosition(1, new Vector3(validPlacementWaypoint.transform.position.x, planeY + 5f, validPlacementWaypoint.transform.position.z));
                }
            }
        }
    }

    private void PlaceTowerInNewPosition()
    {
        if (validPlacementWaypoint == null)
        {
            drag.transform.position = oldTowerPosition;
        }
        else
        {
            newTowerPosition = new Vector3(validPlacementWaypoint.transform.position.x, planeY, validPlacementWaypoint.transform.position.z);
            drag.transform.position = newTowerPosition;
            validPlacementWaypoint.isPlacable = false;
            towerFiring.SetTargeting(true);
            previousPlacementWaypoint = validPlacementWaypoint;
        }
        drag = null;
    }

    public void ResetTowerToStart()
    {
        gameObject.transform.position = initialTowerPosition;
        pivotTransform.rotation = initialTowerRotation;
        validPlacementWaypoint = null;
        oldTowerPosition = initialTowerPosition;
        if (previousPlacementWaypoint != null)
        {
            previousPlacementWaypoint.isPlacable = true; //todo check for no tower placed
        }
        if (towerFiring != null)
        {
            towerFiring.SetTargeting(false);
        }
        gameObject.SetActive(false);
    }
}
