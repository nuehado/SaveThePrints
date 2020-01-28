using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialButton : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZCoordinate;
    [SerializeField] private Transform dialCenter;

    [SerializeField] private List<Button> menuButtons = new List<Button>();
    private Button highlightedButton = null;
    private int highlightedButtonIndex = 0;

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
            if ( highlightedButtonIndex >= menuButtons.Count -1)
            {
                highlightedButtonIndex = 0;
            }
            else
            {
                highlightedButtonIndex++;
            }
            
        }
        if (dialAngle <= -15f && dialAngle > -120f)
        {
            dialCenter.transform.RotateAround(dialCenter.position, dialCenter.up, 30f);

            if (highlightedButtonIndex <= 0)
            {
                highlightedButtonIndex = menuButtons.Count - 1;
            }
            else
            {
                highlightedButtonIndex--;
            }
        }
        highlightedButton = menuButtons[highlightedButtonIndex];
        highlightedButton.Select();
        Debug.Log("selected button " + highlightedButtonIndex);
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void ClickSelectedButton()
    {
        highlightedButton.onClick.Invoke();
    }
}
