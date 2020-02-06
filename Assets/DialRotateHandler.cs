using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRotateHandler : MonoBehaviour
{
    [SerializeField] private Transform dialCenter;
    [SerializeField] private Transform dialNub;
    private Vector3 mouseOffset;
    private float mouseZCoordinate;
    private RadialButton radialButton;
    private float rotateTimer = 0f; // todo remove all of this timer once bug is fixed

    private void OnMouseDrag()
    {
        
        mouseZCoordinate = Camera.main.WorldToScreenPoint(dialNub.transform.position).z;
        mouseOffset = GetMouseAsWorldPoint();
        Vector3 mouseToCenterLine = mouseOffset - dialCenter.transform.position;
        Vector3 dialKnobToCenterLine = dialNub.transform.position - dialCenter.transform.position;
        float dialAngle = Vector3.SignedAngle(mouseToCenterLine, dialKnobToCenterLine, dialCenter.up);
        rotateTimer += Time.deltaTime;

        if (dialAngle >= 18f && dialAngle < 120f && rotateTimer > 1f)
        {
            radialButton = FindObjectOfType<RadialButton>();
            Debug.Log("rotation down from handler" + dialAngle);
            radialButton.HandleDialTurn(true);
            rotateTimer = 0f;
        }

        else if (dialAngle <= -15f && dialAngle > -120f)
        {
            Debug.Log("rotation up from handler" + dialAngle);
            radialButton = FindObjectOfType<RadialButton>();
            radialButton.HandleDialTurn(false);
        }
    }
    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
