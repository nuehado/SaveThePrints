using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;

    void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        Debug.Log("starting patrol...");
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position; //todo change to lerp? or some other smooth transform
            Debug.Log("visiting waypoint: " + waypoint.name);
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("ending patrol...");
    }
}