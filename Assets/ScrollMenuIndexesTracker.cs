using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRectOverride))]

public class ScrollMenuIndexesTracker : MonoBehaviour
{
    public int numberOfVisibleButtons = 4;
    public int largestVisibleButtonIndexIndex;
    public List<int> currentVisibleButtonIndexes;
    public int initialLowestMenuButtonIndex = 0;
    public int currentLargestVisibleButtonIndex;
    public int currentSmallestVisibleButtonIndex;

    private List<int> initialVisibleButtonIndexes = new List<int>();
    
    void Start()
    {
        for (int i = 0 + initialLowestMenuButtonIndex; i < numberOfVisibleButtons + initialLowestMenuButtonIndex; i++)
        {
            initialVisibleButtonIndexes.Add(i);
        }
        currentVisibleButtonIndexes = initialVisibleButtonIndexes;
        largestVisibleButtonIndexIndex = currentVisibleButtonIndexes.Count - 1;
        currentLargestVisibleButtonIndex = currentVisibleButtonIndexes[largestVisibleButtonIndexIndex];
        currentSmallestVisibleButtonIndex = currentVisibleButtonIndexes[0];
    }

    public void UpdateIndexesOnScroll(int upOrDown)
    {
        for (int i = 0; i < numberOfVisibleButtons; i++)
        {
            currentVisibleButtonIndexes[i] += upOrDown;
            currentLargestVisibleButtonIndex = currentVisibleButtonIndexes[largestVisibleButtonIndexIndex];
            currentSmallestVisibleButtonIndex = currentVisibleButtonIndexes[0];
        }
        
    }

}
