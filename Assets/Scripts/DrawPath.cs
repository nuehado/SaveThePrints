using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    LineRenderer pathRenderer;
    private float distanceBetweenPoints;
    private float lineDrawCounter;
    [SerializeField] private float lineDrawSpeed;
    [SerializeField] private EnemySpawner enemySpawner;

    List<Waypoint> path;

    private int iWaypoint;
    private int iNextWaypoint;
    private int iNextRenderPoint;

    [SerializeField] Transform extruder;
    [SerializeField] Transform printerStaticParts;

    [SerializeField] private AudioSource bedMoving;
    private bool isPrintBedMoving = false;

    private void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.GetPath();
        pathRenderer = GetComponent<LineRenderer>();
        path = pathfinder.GetPath();
        distanceBetweenPoints = 10f; //Vector3.Distance(path[i].transform.position, path[i + 1].transform.position);
        pathRenderer.positionCount = 2;
        iWaypoint = path.Count-1;
        iNextWaypoint = path.Count-2;
        iNextRenderPoint = 1;
        pathRenderer.SetPosition(0, path[iWaypoint].transform.position);
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void FixedUpdate()
    {
        if (pathRenderer.positionCount < path.Count + 1)
        {
            DrawRenderedPath();
        }
    }

    private void DrawRenderedPath()
    {
        lineDrawCounter += 0.1f / lineDrawSpeed;
        float lerpedDistance = Mathf.Lerp(0f, distanceBetweenPoints, lineDrawCounter);
        Vector3 lengthTowardsNextWaypoint = lerpedDistance * Vector3.Normalize(path[iNextWaypoint].transform.position - path[iWaypoint].transform.position) + path[iWaypoint].transform.position;
        pathRenderer.SetPosition(iNextRenderPoint, lengthTowardsNextWaypoint + new Vector3(0f, 0.1f, 0f));
        StartPrinterPathTranslations(lengthTowardsNextWaypoint);

        if (lineDrawCounter >= 1f)
        {
            iWaypoint--;
            iNextWaypoint--;
            lerpedDistance = 0f;
            lineDrawCounter = 0f;
            pathRenderer.positionCount++;
            iNextRenderPoint++;
            pathRenderer.SetPosition(iNextRenderPoint, path[iWaypoint].transform.position + new Vector3(0f, 0.1f, 0f));
        }

        if (pathRenderer.positionCount >= path.Count + 1)
        {
            enemySpawner.startSpawningExternal();
            StopPrinterPathTranslations();
        }

    }

    private void StartPrinterPathTranslations(Vector3 lengthTowardsNextWaypoint)
    {
        extruder.transform.position = new Vector3(lengthTowardsNextWaypoint.x, extruder.position.y, extruder.position.z);
        printerStaticParts.transform.position = new Vector3(printerStaticParts.transform.position.x, printerStaticParts.transform.position.y, lengthTowardsNextWaypoint.z);
        if (isPrintBedMoving == false)
        {
            bedMoving.Play();
            isPrintBedMoving = true;
        }
    }

    private void StopPrinterPathTranslations()
    {
        bedMoving.Stop();
        isPrintBedMoving = false;
    }
}
