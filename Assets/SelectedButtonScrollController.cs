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
    private int up = 1;
    private int down = -1;

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
    }

    private void CheckIfSelectedButtonOutsideView()
    {
        if (currentlySelectedButtonIndex > currentlyDisplayedButtonIndexes[maxMenuPositionIndex])
        {
            scrollStepHandler.UpdateScrollPosition(down);
            UpdateDisplayIndexes(down);
        }
        else if (currentlySelectedButtonIndex < currentlyDisplayedButtonIndexes[0])
        {
            scrollStepHandler.UpdateScrollPosition(up);
            UpdateDisplayIndexes(up);
        }
    }

    private void UpdateDisplayIndexes(int scrollDirection)
    {
        for (int j = 0; j <= maxMenuPositionIndex; j++)
        {
            currentlyDisplayedButtonIndexes[j] -= scrollDirection;
        }
    }
}
