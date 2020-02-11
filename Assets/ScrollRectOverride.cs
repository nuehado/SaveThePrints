using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectOverride : ScrollRect, IScrollHandler
{
    private SelectedButtonScrollController selectedButtonScrollController;
    private int currentlySelectedButtonIndex;
    private MenuButton[] buttons;

    private new void Start()
    {
        base.Start();
        selectedButtonScrollController = GetComponent<SelectedButtonScrollController>();
    }
    public override void OnScroll(PointerEventData data)
    {
        base.OnScroll(data);

        currentlySelectedButtonIndex = selectedButtonScrollController.currentlySelectedButtonIndex;
        buttons = selectedButtonScrollController.buttons;
        if (data.scrollDelta.y < 0 && currentlySelectedButtonIndex < buttons.Length - 1)
        {
            int newButtonSelectionIndex = currentlySelectedButtonIndex + 1;
            buttons[newButtonSelectionIndex].SelectButton();
            
        }
        else if (data.scrollDelta.y > 0 && currentlySelectedButtonIndex > 0)
        {
            int newButtonSelectionIndex = currentlySelectedButtonIndex - 1;
            buttons[newButtonSelectionIndex].SelectButton();
        }
    }
}
