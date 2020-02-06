using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraOnSceneLoad : MonoBehaviour
{
    private Vector3 levelViewPos = new Vector3(-22f, 8.5f, 331f);
    private bool isCameraToMove;
    [SerializeField] private float cameraMoveSpeed = 0.1f;

    private void Update()
    {
        if (isCameraToMove == true)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, levelViewPos, cameraMoveSpeed * Time.deltaTime);
        }
    }

    public void MoveCamera()
    {
        isCameraToMove = true;
    }
}
