using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDisplayedIndexes : MonoBehaviour
{
    public int[] currentlyDisplayedButtonIndexes = new int[] { 0, 1, 2, 3 };
    public int maxMenuPositionIndex = 3;
    [Range(0f, 1f)] private float previousScrollYPosition = 1;
    private int up = 1;
    private int down = -1;
    private MenuButton[] menuButtons;

    private void Start()
    {
        menuButtons = GetComponentsInChildren<MenuButton>();
    }

    public void UpdateDisplayIndexes(Vector2 scrollPosition)
    {
        float scrollYPosition = scrollPosition.y;
        if (scrollYPosition < 0.001f)
        {
            scrollYPosition = 0.001f;
        }
        
        if (scrollYPosition > previousScrollYPosition && currentlyDisplayedButtonIndexes[0] > 0)
        {
            ChangeIndexValues(up);
        }
        else if (scrollYPosition < previousScrollYPosition && currentlyDisplayedButtonIndexes[3] < menuButtons.Length - 1)
        {
            ChangeIndexValues(down);
        }
        previousScrollYPosition = scrollYPosition;
    }

    private void ChangeIndexValues(int scrollYDirection)
    {
        for (int j = 0; j <= maxMenuPositionIndex; j++)
        {
            currentlyDisplayedButtonIndexes[j] -= scrollYDirection;
        }
    }

}
