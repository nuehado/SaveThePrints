using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedButtonScrollController : MonoBehaviour
{
    private ScrollMenuIndexesTracker scrollMenuIndexesTracker;
    public MenuButton[] buttons;
    public int currentlySelectedButtonIndex;
    private ScrollRectOverride scrollRectOverride;
    private float scrollStepDistance;
    private float scrollVerticalNormalizedPosition = 1f;

    private List<int> currentVisibleButtonIndexes;

    private void Start()
    {
        scrollMenuIndexesTracker = GetComponent<ScrollMenuIndexesTracker>();
        scrollRectOverride = GetComponent<ScrollRectOverride>();
        buttons = GetComponentsInChildren<MenuButton>();
        scrollStepDistance = 1f / (buttons.Length - scrollMenuIndexesTracker.numberOfVisibleButtons);
        buttons[scrollMenuIndexesTracker.initialLowestMenuButtonIndex].SelectButton();
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

        }
        else if (currentlySelectedButtonIndex > scrollMenuIndexesTracker.currentLargestVisibleButtonIndex)
        {
            scrollRectOverride.verticalNormalizedPosition -= scrollStepDistance;
            scrollMenuIndexesTracker.UpdateIndexesOnScroll(1);
        }
    }
}
