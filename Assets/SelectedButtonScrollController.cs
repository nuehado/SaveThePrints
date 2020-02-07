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

    private void Start()
    {
        scrollStepHandler = GetComponent<ScrollStepHandler>();
    }

    public void SetMenuPosition(float upOrDown)
    {
        if (upOrDown > 0 && menuPositionIndex > 0)
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
        }
    }
}
