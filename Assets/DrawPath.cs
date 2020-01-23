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

    private void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.GetPath();
        pathRenderer = GetComponent<LineRenderer>();
        path = pathfinder.GetPath();
        distanceBetweenPoints = 10f; //Vector3.Distance(path[i].transform.position, path[i + 1].transform.position);
        pathRenderer.positionCount = 2;
        iWaypoint = 0;
        iNextWaypoint = 1;
        pathRenderer.SetPosition(iWaypoint, path[iWaypoint].transform.position);

        enemySpawner = FindObjectOfType<EnemySpawner>();

    }

    // Update is called once per frame
    void Update()
    {
        if (pathRenderer.positionCount < path.Count)
        {
            DrawRenderedPath();
        }
        else
        {
            
        }
        
    }

    private void DrawRenderedPath()
    {
        /*for (int i = 0; i < path.Count - 1; i++)
        {
            
            if (lineDrawCounter < distanceBetweenPoints)
            {
                lineDrawCounter += 0.1f / lineDrawSpeed;
                float lerpedDistance1 = Mathf.Lerp(0f, distanceBetweenPoints, lineDrawCounter);
                Vector3 pointAlongLine = lerpedDistance1 * Vector3.Normalize(path[i + 1].transform.position - path[i].transform.position) + path[i].transform.position;
                pathRenderer.SetPosition(i, pointAlongLine + new Vector3(0f, 0.1f, 0f));
                if (Vector3.Distance(pathRenderer.GetPosition(i), path[destinationPoint].transform.position) < movementCompleteDistance)
                {
                    UpdateMovementTarget();
                }
            }


        }*/

        lineDrawCounter += 0.1f / lineDrawSpeed;
        float lerpedDistance = Mathf.Lerp(0f, distanceBetweenPoints, lineDrawCounter);
        Vector3 lengthTowardsNextWaypoint = lerpedDistance * Vector3.Normalize(path[iNextWaypoint].transform.position - path[iWaypoint].transform.position) + path[iWaypoint].transform.position;
        pathRenderer.SetPosition(iNextWaypoint, lengthTowardsNextWaypoint + new Vector3(0f,0.1f,0f));
        Debug.Log(lineDrawCounter);
        Debug.Log(distanceBetweenPoints);
        if (lineDrawCounter >= 1f)
        {
            Debug.Log("waypoint found");
            iWaypoint ++;
            iNextWaypoint ++;
            lerpedDistance = 0f;
            lineDrawCounter = 0f;
            pathRenderer.positionCount ++;
            pathRenderer.SetPosition(iNextWaypoint, path[iWaypoint].transform.position);

        }

        if (pathRenderer.positionCount >= path.Count)
        {
            enemySpawner.startSpawningExternal();
        }
        
    }
}
