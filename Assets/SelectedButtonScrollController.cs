using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedButtonScrollController : MonoBehaviour
{
    private ScrollMenuIndexesTracker scrollMenuIndexesTracker;
    private MenuButton[] buttons;
    private int currentlySelectedButtonIndex;
    private ScrollRectOverride scrollRectOverride;
    private float scrollStepDistance;
    private float scrollVerticalNormalizedPosition = 1f;

    private ScrollStepHandler scrollStepHandler;
    [Range(0,3)] public int menuPositionIndex = 0;
    //private CurrentDisplayedIndexes currentDisplayedIndexes;
    //private int[] currentlyDisplayedButtonIndexes;
    
    //private int up = 1;
    //private int down = -1;
    //private int upIndex = 0;
    //private int downIndex = 3;
    //[Range(0f, 1f)] private float previousScrollYPosition = 1;
    //public bool isScrolling = true;

    private List<int> currentVisibleButtonIndexes;

    private void Start()
    {
        scrollMenuIndexesTracker = GetComponent<ScrollMenuIndexesTracker>();
        scrollRectOverride = GetComponent<ScrollRectOverride>();
        buttons = GetComponentsInChildren<MenuButton>();
        scrollStepDistance = 1f / (buttons.Length - scrollMenuIndexesTracker.numberOfVisibleButtons);
        scrollStepHandler = GetComponent<ScrollStepHandler>();
        //currentDisplayedIndexes = GetComponent<CurrentDisplayedIndexes>();
        //currentlyDisplayedButtonIndexes = currentDisplayedIndexes.currentlyDisplayedButtonIndexes;
    }

    public void SetMenuPosition(Button selectedButton)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<Button>() == selectedButton)
            {
                currentlySelectedButtonIndex = i;
                Debug.Log("we found a button match");
            }
        }
        Debug.Log("currently selected button index: " + currentlySelectedButtonIndex);
        CheckIfSelectedButtonOutsideView();
    }

    private void CheckIfSelectedButtonOutsideView()
    {
        currentVisibleButtonIndexes = scrollMenuIndexesTracker.currentVisibleButtonIndexes;
        if (currentlySelectedButtonIndex < scrollMenuIndexesTracker.currentSmallestVisibleButtonIndex)
        {
            scrollRectOverride.verticalNormalizedPosition += scrollStepDistance;
            scrollMenuIndexesTracker.UpdateIndexesOnScroll(-1);

            //scrollStepHandler.UpdateScrollPosition(up);
            //isScrolling = false;
        }
        else if (currentlySelectedButtonIndex > scrollMenuIndexesTracker.currentLargestVisibleButtonIndex)
        {
            scrollRectOverride.verticalNormalizedPosition -= scrollStepDistance;
            scrollMenuIndexesTracker.UpdateIndexesOnScroll(1);

            //scrollStepHandler.UpdateScrollPosition(down);
            //isScrolling = false;
        }
    }

    /*public void SelectNewButtonIfOutsideView(Vector2 scrollPosition)
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

        if (scrollYPosition < previousScrollYPosition && currentlyDisplayedButtonIndexes[3] < buttons.Length)
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
            if (buttons[i].gameObject == EventSystem.current.currentSelectedGameObject)
            {
                Debug.Log("selected button: " + buttons[i]);
                isVisibleButtonSelected = true;
            }
        }

        if (isVisibleButtonSelected == false)
        {
            buttons[currentlyDisplayedButtonIndexes[upOrDownIndex]].SelectButton();
        }
    }*/
}
