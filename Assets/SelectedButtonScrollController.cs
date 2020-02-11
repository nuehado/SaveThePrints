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
    public int previouslySelectedButtonIndex;
    private ScrollRectOverride scrollRectOverride;
    private float scrollStepDistance;
    private float scrollVerticalNormalizedPosition = 1f;
    private DialRotateHandler dialRotateHandler;

    private List<int> currentVisibleButtonIndexes;

    private void Awake()
    {
        scrollMenuIndexesTracker = GetComponent<ScrollMenuIndexesTracker>();
        scrollRectOverride = GetComponent<ScrollRectOverride>();
        buttons = GetComponentsInChildren<MenuButton>();
        scrollStepDistance = 1f / (buttons.Length - scrollMenuIndexesTracker.numberOfVisibleButtons);
        dialRotateHandler = FindObjectOfType<DialRotateHandler>();
        
        buttons[scrollMenuIndexesTracker.initialLowestMenuButtonIndex].SelectButton();
    }

    private void OnEnable()
    {
        dialRotateHandler.selectedButtonScrollController = this;
        buttons[scrollMenuIndexesTracker.initialLowestMenuButtonIndex].SelectButton();
    }

    public void SetMenuPosition(Button selectedButton)
    {
        previouslySelectedButtonIndex = currentlySelectedButtonIndex;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<Button>() == selectedButton)
            {
                currentlySelectedButtonIndex = i;
            }
        }
        CheckIfSelectedButtonOutsideView();
        dialRotateHandler.CheckIfDialTransformRotateNeeded(previouslySelectedButtonIndex - currentlySelectedButtonIndex);
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

    private void OnDisable()
    {
        buttons[currentlySelectedButtonIndex].GetComponent<MenuButton>().SetText();
    }
}
