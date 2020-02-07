using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollStepHandler : MonoBehaviour
{
    private ScrollRect scrollRect;
    public Button[] menuButtons;
    private float scrollStepDistance;
    private float scrollVerticalNormalizedPosition = 1f;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        menuButtons = GetComponentsInChildren<Button>();
        scrollStepDistance = 1f / (menuButtons.Length - 4);
    }
    public void UpdateScrollPosition(float upOrDown)
    {
        scrollVerticalNormalizedPosition += upOrDown * scrollStepDistance;
        CheckForScrollOutsideClamp();
        scrollRect.verticalNormalizedPosition = scrollVerticalNormalizedPosition;
    }

    private void CheckForScrollOutsideClamp()
    {
        if (scrollVerticalNormalizedPosition >= 1f)
        {
            scrollVerticalNormalizedPosition = 1f;
        }
        if (scrollVerticalNormalizedPosition <= 0f)
        {
            scrollVerticalNormalizedPosition = 0f;
        }
    }
}
