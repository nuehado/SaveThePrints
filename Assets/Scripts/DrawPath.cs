﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    LineRenderer pathLineRenderer;
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
    private bool isLevelLoaded = false;
    private bool isFirstLevelLoad = true;

    private GameObject[] towers;
    private GameObject[] defenseSupports;
    private GameObject slowStick;
    private DefensesStore defensesStore;
    private DefenseHoverOutliner defenseHoverer;

    [SerializeField] private GameObject loadScreen;
    [SerializeField] private GameObject LevelScreen;
    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        towers = GameObject.FindGameObjectsWithTag("Tower");
        defenseSupports = GameObject.FindGameObjectsWithTag("DefenseSupport");
        slowStick = GameObject.FindGameObjectWithTag("SlowStick");
        defensesStore = FindObjectOfType<DefensesStore>();
        defenseHoverer = FindObjectOfType<DefenseHoverOutliner>();
    }
    private void OnEnable()
    {
        Pathfinder pathfinder = GetComponent<Pathfinder>(); 
        path = pathfinder.GetPath();
        pathLineRenderer = GetComponent<LineRenderer>();
        distanceBetweenPoints = 10f; 
        pathLineRenderer.positionCount = 2;
        iWaypoint = path.Count - 1;
        iNextWaypoint = path.Count - 2;
        iNextRenderPoint = 1;
        pathLineRenderer.SetPosition(0, path[iWaypoint].transform.position);
        isLevelLoaded = true;
    }

    void FixedUpdate()
    {
        if (isLevelLoaded)
        {
            if (pathLineRenderer.positionCount < path.Count + 1)
            {
                DrawRenderedPath();
            }
        }
    }

    private void DrawRenderedPath()
    {
        lineDrawCounter += 0.1f / lineDrawSpeed;
        float lerpedDistance = Mathf.Lerp(0f, distanceBetweenPoints, lineDrawCounter);
        Vector3 lengthTowardsNextWaypoint = lerpedDistance * Vector3.Normalize(path[iNextWaypoint].transform.position - path[iWaypoint].transform.position) + path[iWaypoint].transform.position;
        pathLineRenderer.SetPosition(iNextRenderPoint, lengthTowardsNextWaypoint + new Vector3(0f, -0.3f, 0f));
        StartPrinterPathTranslations(lengthTowardsNextWaypoint);

        if (lineDrawCounter >= 1f)
        {
            iWaypoint--;
            iNextWaypoint--;
            lerpedDistance = 0f;
            lineDrawCounter = 0f;
            pathLineRenderer.positionCount++;
            iNextRenderPoint++;
            pathLineRenderer.SetPosition(iNextRenderPoint, path[iWaypoint].transform.position + new Vector3(0f, -0.3f, 0f));
        }

        if (pathLineRenderer.positionCount >= path.Count + 1)
        {
            enemySpawner.startSpawningExternal();
            StopPrinterPathTranslations();
            EnableDefenseMovers();

            foreach (GameObject defenseSupport in defenseSupports)
            {
                defenseSupport.GetComponent<DefenseSupportMover>().enabled = true;
            }
            
            loadScreen.SetActive(false);
            LevelScreen.SetActive(true);
        }

    }

    private void EnableDefenseMovers()
    {
        towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject activeDefenses in defensesStore.purchasedDefenses)
        {
            if (activeDefenses.tag == "Tower")
            {
                activeDefenses.GetComponent<TowerMover>().enabled = true;
            }
            if (activeDefenses.tag == "DefenseSupport")
            {
                activeDefenses.GetComponent<DefenseSupportMover>().enabled = true;
            }
            if (activeDefenses.tag == "SlowStick")
            {
                activeDefenses.GetComponent<SlowStickMover>().enabled = true;
            }
        }
        defenseHoverer.GetComponent<DefenseHoverOutliner>().enabled = true;
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
