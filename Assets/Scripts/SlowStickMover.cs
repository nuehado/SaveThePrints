using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowStickMover : MonoBehaviour
{
    private GameObject drag;
    private Vector3 updatePosition;
    private float planeY = -0.75f;
    private Plane plane;
    private Ray ray;
    private Vector3 newStickPosition;
    private Vector3 oldStickPosition;
    public Vector3 initialStickPosition;

    private LineRenderer checkLineRenderer;
    private Waypoint waypointStickIsOver;
    private Waypoint previousPlacementWaypoint = null;
    private Waypoint validPlacementWaypoint;
    private bool isStickDepleted = false;
    private int stickUsedCount = 0;

    [SerializeField] private SlowEffect[] slowAreas;
    [SerializeField] private AudioSource selectedSFX;
    private AudioSource placedSFX;

    private void Start()
    {
        plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane
        initialStickPosition = (gameObject.transform.position);
        oldStickPosition = initialStickPosition;
        checkLineRenderer = GetComponent<LineRenderer>();
        placedSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectStick();
        }

        if (Input.GetMouseButton(0) && drag != null)
        {
            DragStick();
        }

        if (Input.GetMouseButtonUp(0) && drag != null)
        {
            ApplyStickInNewPosition();
        }
            
        if (drag == null)
        {
            checkLineRenderer.enabled = false;
        }
    }

    private void SelectStick()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500.0f))
        {
            if (gameObject == hit.transform.gameObject && isStickDepleted == false)
            {
                drag = hit.transform.gameObject;
                selectedSFX.Play();
}
        }
    }

    private void DragStick()
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
                waypointStickIsOver = waypointOver.GetComponent<Waypoint>();

                if (previousPlacementWaypoint != null)
                {
                    previousPlacementWaypoint.isOnPath = true;
                }

                if (waypointStickIsOver.isOnPath == true)
                {
                    validPlacementWaypoint = waypointStickIsOver;
                    checkLineRenderer.enabled = true;
                    checkLineRenderer.SetPosition(0, new Vector3(validPlacementWaypoint.transform.position.x, planeY, validPlacementWaypoint.transform.position.z));
                    checkLineRenderer.SetPosition(1, new Vector3(validPlacementWaypoint.transform.position.x, planeY + 5f, validPlacementWaypoint.transform.position.z));
                }
            }
        }
    }

    private void ApplyStickInNewPosition()
    {
        if (validPlacementWaypoint == null)
        {
            drag.transform.position = oldStickPosition;
        }
        else
        {
            newStickPosition = new Vector3(validPlacementWaypoint.transform.position.x, planeY, validPlacementWaypoint.transform.position.z);
            for (int i = 0; i < slowAreas.Length; i += 1)
            {
                if (slowAreas[i].isNew == true)
                {
                    slowAreas[i].transform.position = newStickPosition;
                    slowAreas[i].isNew = false;
                    //todo add animation of stick application
                    placedSFX.Play();
                    validPlacementWaypoint.isOnPath = false;
                    previousPlacementWaypoint = validPlacementWaypoint;
                    gameObject.transform.position = initialStickPosition;
                    drag = null;
                    return;
                }
                else
                {
                    isStickDepleted = true;
                    GetComponentInChildren<MeshRenderer>().enabled = false;
                }
            } 
        }
        gameObject.transform.position = initialStickPosition;
        drag = null;
    }

    public void ResetStickToStart()
    {
        checkLineRenderer.enabled = false;
        GetComponent<OutlineEnabler>().enabled = false;
        drag = null;
        gameObject.transform.position = initialStickPosition;
        GetComponentInChildren<MeshRenderer>().enabled = true;
        validPlacementWaypoint = null;
        oldStickPosition = initialStickPosition;
        isStickDepleted = false;
        foreach (SlowEffect slowEffect in slowAreas)
        {
            slowEffect.ResetSlowEffect();
        }
        if (previousPlacementWaypoint != null)
        {
            previousPlacementWaypoint.isOnPath = true; 
        }
        gameObject.SetActive(false);
    }
}
