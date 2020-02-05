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

    [SerializeField] private ScrollRect menuScroller;
    private int scrollIncrements = 0;
    private float scrollIncrementDistance;
    private int menuPositionIndex = 0;

    private void Start()
    {
        menuScroller.verticalNormalizedPosition = 1f;
        scrollIncrements = menuButtons.Count - 4;
        scrollIncrementDistance = 1f / scrollIncrements;

    }

    private void OnMouseDrag()
    {
        mouseZCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset =  GetMouseAsWorldPoint();
        Vector3 mouseToCenterLine = mouseOffset - dialCenter.position;
        Vector3 dialKnobToCenterLine = gameObject.transform.position - dialCenter.position;
        float dialAngle = Vector3.SignedAngle(mouseToCenterLine, dialKnobToCenterLine, dialCenter.up);
        
        if (dialAngle >= 15f && dialAngle < 120f)
        {
            RotateDialTransformCounterClockwise();
            ScrollMenuButtonSelectDown();
        }

        if (dialAngle <= -15f && dialAngle > -120f)
        {
            RotateDialTransformClockwise();
            ScrollMenuButtonSelectUp();
        }

        highlightedButton = menuButtons[highlightedButtonIndex];
        highlightedButton.Select();
        Debug.Log("selected button " + highlightedButtonIndex);
    }

    private void RotateDialTransformClockwise()
    {
        dialCenter.transform.RotateAround(dialCenter.position, dialCenter.up, 18f);
    }

    private void RotateDialTransformCounterClockwise()
    {
        dialCenter.transform.RotateAround(dialCenter.position, dialCenter.up, -18f);
    }
    private void ScrollMenuButtonSelectDown()
    {
        if (highlightedButtonIndex >= menuButtons.Count - 1)
        {
            menuScroller.verticalNormalizedPosition = 0f;
            return;
        }
        else
        {
            if (menuPositionIndex < 4)
            {
                menuPositionIndex++;
            }

            highlightedButtonIndex++;
            if (highlightedButtonIndex >= 4 && menuPositionIndex == 4)
            {
                menuScroller.verticalNormalizedPosition -= scrollIncrementDistance;
            }
        }
    }

    private void ScrollMenuButtonSelectUp()
    {
        if (highlightedButtonIndex <= 0)
        {
            menuScroller.verticalNormalizedPosition = 1f;
            return;
        }
        else
        {
            if (menuPositionIndex > 0)
            {
                menuPositionIndex--;
            }

            highlightedButtonIndex--;
            if (highlightedButtonIndex <= menuButtons.Count - 4 && menuPositionIndex == 0)
            {
                menuScroller.verticalNormalizedPosition += scrollIncrementDistance;
            }
        }
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
