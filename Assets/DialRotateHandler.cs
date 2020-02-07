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

    private void OnMouseDrag()
    {
        
        mouseZCoordinate = Camera.main.WorldToScreenPoint(dialNub.transform.position).z;
        mouseOffset = GetMouseAsWorldPoint();
        Vector3 mouseToCenterLine = mouseOffset - dialCenter.transform.position;
        Vector3 dialKnobToCenterLine = dialNub.transform.position - dialCenter.transform.position;
        float dialAngle = Vector3.SignedAngle(mouseToCenterLine, dialKnobToCenterLine, dialCenter.up);

        if (dialAngle <= -15f && dialAngle > -120f)
        {
            Debug.Log("rotation up from handler" + dialAngle);
            radialButton = FindObjectOfType<RadialButton>();
            radialButton.HandleDialTurn(false);
        }
        else if (dialAngle >= 18f && dialAngle < 120f)
        {
            radialButton = FindObjectOfType<RadialButton>();
            Debug.Log("rotation down from handler" + dialAngle);
            radialButton.HandleDialTurn(true);
        }
    }
    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
