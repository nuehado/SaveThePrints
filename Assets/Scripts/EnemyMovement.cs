using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //[SerializeField] List<Waypoint> path;

    void Start()
    {
        
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    private IEnumerator FollowPath(List<Waypoint> path)
    {
        Debug.Log("starting patrol...");
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position; //todo change to lerp? or some other smooth transform
            Debug.Log("visiting waypoint: " + waypoint.name);
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("ending patrol...");
    }
}