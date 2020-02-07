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
    private MenuButton[] menuButtons;
    private CurrentDisplayedIndexes currentDisplayedIndexes;
    private int[] currentlyDisplayedButtonIndexes;
    private int currentlySelectedButtonIndex = 0;
    private int up = 1;
    private int down = -1;
    private int upIndex = 0;
    private int downIndex = 3;
    [Range(0f, 1f)] private float previousScrollYPosition = 1;
    public bool isScrolling = true;

    private void Start()
    {
        scrollStepHandler = GetComponent<ScrollStepHandler>();
        menuButtons = GetComponentsInChildren<MenuButton>();
        currentDisplayedIndexes = GetComponent<CurrentDisplayedIndexes>();
        currentlyDisplayedButtonIndexes = currentDisplayedIndexes.currentlyDisplayedButtonIndexes;
    }

    public void SetMenuPosition(MenuButton selectedButton)
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (menuButtons[i] == selectedButton)
            {
                currentlySelectedButtonIndex = i;
            }
        }

        ScrollIfSelectedButtonOutsideView();
    }

    private void ScrollIfSelectedButtonOutsideView()
    {
        if (currentlySelectedButtonIndex > currentlyDisplayedButtonIndexes[currentDisplayedIndexes.maxMenuPositionIndex])
        {
            scrollStepHandler.UpdateScrollPosition(down);
            isScrolling = false;
        }
        else if (currentlySelectedButtonIndex < currentlyDisplayedButtonIndexes[0])
        {
            scrollStepHandler.UpdateScrollPosition(up);
            isScrolling = false;
        }
    }

    public void SelectNewButtonIfOutsideView(Vector2 scrollPosition)
    {
        if (isScrolling == false)
        {
            isScrolling = true;
            return;
        }
        currentlyDisplayedButtonIndexes = GetComponent<CurrentDisplayedIndexes>().currentlyDisplayedButtonIndexes;
        float scrollYPosition = scrollPosition.y;
        if (scrollYPosition < 0.001f)
        {
            scrollYPosition = 0.001f;
        }

        if (scrollYPosition > previousScrollYPosition && currentlyDisplayedButtonIndexes[0] > 0)
        {
            CheckForSelectionNeeded(upIndex);
        }

        if (scrollYPosition < previousScrollYPosition && currentlyDisplayedButtonIndexes[3] < menuButtons.Length)
        {
            CheckForSelectionNeeded(downIndex);
        }
        previousScrollYPosition = scrollYPosition;
    }

    private void CheckForSelectionNeeded(int upOrDownIndex)
    {
        bool isVisibleButtonSelected = false;
        for (int i = currentlyDisplayedButtonIndexes[0]; i < currentlyDisplayedButtonIndexes[3]; i++)
        {
            if (menuButtons[i].gameObject == EventSystem.current.currentSelectedGameObject)
            {
                Debug.Log("selected button: " + menuButtons[i]);
                isVisibleButtonSelected = true;
            }
        }

        if (isVisibleButtonSelected == false)
        {
            menuButtons[currentlyDisplayedButtonIndexes[upOrDownIndex]].SelectButton();
        }
    }
}
