using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialButton : MonoBehaviour
{
    
    [SerializeField] private Transform dialCenter;

    public List<Button> menuButtons = new List<Button>();
    private Button highlightedButton = null;
    public int highlightedButtonIndex = 0;

    [SerializeField] private ScrollRect menuScroller;
    private int scrollIncrements = 0;
    private float scrollIncrementDistance;
    public int menuPositionIndex = 0;
    private float previousMenuScrollerYPosition;

    private void Start()
    {
        if (menuScroller != null)
        {
            menuScroller.verticalNormalizedPosition = 1f;
            scrollIncrements = menuButtons.Count - 4;
            scrollIncrementDistance = 1f / scrollIncrements;
            menuScroller.onValueChanged.AddListener(RotateDialFromMouseScroll);
        }
        highlightedButton = menuButtons[0];
        highlightedButton.Select();
    }

    public void HandleDialTurn(bool isRotatedCounter)
    {
        if (isRotatedCounter)
        {
            ScrollMenuButtonSelectDown();
        }

        else if (!isRotatedCounter)
        {
            ScrollMenuButtonSelectUp();
        }
        SelectMenuButton();
    }

    private void SelectMenuButton()
    {
        //Debug.Log("highlight index: " + highlightedButtonIndex);
        highlightedButton = menuButtons[highlightedButtonIndex];
        highlightedButton.Select();
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
            menuPositionIndex = 3;
            RotateDialTransformCounterClockwise();
            return;
        }
        else
        {
            if (menuPositionIndex < 3)
            {
                menuPositionIndex++;
            }
            if (highlightedButtonIndex >= 3 && menuPositionIndex == 3)
            {
                menuScroller.verticalNormalizedPosition -= scrollIncrementDistance;
            }
            else
            {
                RotateDialTransformCounterClockwise();
            }
            
            highlightedButtonIndex++;
        }
    }

    private void ScrollMenuButtonSelectUp()
    {
        if (highlightedButtonIndex <= 0)
        {
            menuScroller.verticalNormalizedPosition = 1f;
            menuPositionIndex = 0;
            RotateDialTransformClockwise();
            return;
        }
        else
        {
            highlightedButtonIndex--;
            if (highlightedButtonIndex <= menuButtons.Count - 4 && menuPositionIndex == 0)
            {
                menuScroller.verticalNormalizedPosition += scrollIncrementDistance;
            }
            else
            {
                RotateDialTransformClockwise();
            }
            if (menuPositionIndex > 0)
            {
                menuPositionIndex--;
            }
        }
    }

    

    public void ClickSelectedButton()
    {
        highlightedButton.onClick.Invoke();
    }

    public void RotateDialFromMouseScroll(Vector2 value)
    {
        float currentMenuScrollerYPosition = value.y;
        if (Mathf.Abs(currentMenuScrollerYPosition - previousMenuScrollerYPosition) < 0.001)
        {
            previousMenuScrollerYPosition = currentMenuScrollerYPosition;
            return;
        }
        
        if (currentMenuScrollerYPosition < previousMenuScrollerYPosition)
        {
            RotateDialTransformCounterClockwise();
        }
        else
        {
            RotateDialTransformClockwise();
        }
        previousMenuScrollerYPosition = currentMenuScrollerYPosition;
    }
}
