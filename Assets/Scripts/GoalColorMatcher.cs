using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalColorMatcher : MonoBehaviour
{
    private RGBColorChanger colorChanger;

    private void Awake()
    {
        colorChanger = FindObjectOfType<RGBColorChanger>();
    }

    private void OnEnable()
    {
        GetComponent<MeshRenderer>().material.SetColor("_Color", colorChanger.lastPrintColor);
    }
}
