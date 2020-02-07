using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedButtonScrollController : MonoBehaviour
{
    private ScrollStepHandler scrollStepHandler;
    [Range(0,3)] public int menuPositionIndex = 0;
    const int maxMenuPositionIndex = 3;
    private MenuButton[] menuButtons;
    private int[] currentlyDisplayedButtonIndexes = new int[] { 0, 1, 2, 3 };
    private int currentlySelectedButtonIndex = 0;

    //array of 4 button indexes currently on display
    //when a new button is selected
    //if it's index is greater than the largest button index in array, shift array up one (unless at max)
    //if it's index is less than the smallest button index in the array, shift array down onw (unless at min)
    //todo change OnMove event on each button to OnSelect. this should make it independent of selection method

    private void Start()
    {
        scrollStepHandler = GetComponent<ScrollStepHandler>();
        menuButtons = GetComponentsInChildren<MenuButton>();

    }

    public void SetMenuPosition(MenuButton selectedButton)
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (menuButtons[i].gameObject == selectedButton.gameObject)
            {
                currentlySelectedButtonIndex = i;
            }
        }

        CheckIfSelectedButtonOutsideView();
        
        
        /*if (upOrDown > 0 && menuPositionIndex > 0)
        {
            menuPositionIndex--;
        }
        else if ( upOrDown < 0 && menuPositionIndex < maxMenuPositionIndex)
        {
            menuPositionIndex++;
        }
        else
        {
            scrollStepHandler.UpdateScrollPosition(upOrDown);
        }*/
    }

    private void CheckIfSelectedButtonOutsideView()
    {
        if (currentlySelectedButtonIndex > currentlyDisplayedButtonIndexes[maxMenuPositionIndex])
        {
            scrollStepHandler.UpdateScrollPosition(-1f);
            for (int j = 0; j <= maxMenuPositionIndex; j++)
            {
                currentlyDisplayedButtonIndexes[j]++;
            }
        }
        else if (currentlySelectedButtonIndex < currentlyDisplayedButtonIndexes[0])
        {
            scrollStepHandler.UpdateScrollPosition(1f);
            for (int j = 0; j <= maxMenuPositionIndex; j++)
            {
                currentlyDisplayedButtonIndexes[j]--;
            }
        }
    }
}
