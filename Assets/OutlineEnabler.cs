using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class OutlineEnabler : MonoBehaviour
{
    [SerializeField] Outline[] containedOutlines;

    private void OnEnable()
    {
        foreach(Outline outline in containedOutlines)
        {
            outline.enabled = true;
        }
    }

    private void OnDisable()
    {
        foreach (Outline outline in containedOutlines)
        {
            outline.enabled = false;
        }
    }
}
