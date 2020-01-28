using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialButton : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZCoordinate;
    [SerializeField] private Transform dialCenter;

    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    private Button highlightedButton = null;

    private void OnMouseDrag()
    {
        mouseZCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset =  GetMouseAsWorldPoint();
        Vector3 mouseToCenterLine = mouseOffset - dialCenter.position;
        Vector3 dialKnobToCenterLine = gameObject.transform.position - dialCenter.position;
        float dialAngle = Vector3.SignedAngle(mouseToCenterLine, dialKnobToCenterLine, dialCenter.up);
        
        if (dialAngle >= 15f && dialAngle < 120f)
        {
            dialCenter.transform.RotateAround(dialCenter.position, dialCenter.up, -30f);
            button1.Select();
            
        }
        if (dialAngle <= -15f && dialAngle > -120f)
        {
            {
                dialCenter.transform.RotateAround(dialCenter.position, dialCenter.up, 30f);
            }
        }
        
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
