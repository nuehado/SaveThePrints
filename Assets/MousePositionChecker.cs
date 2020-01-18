using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionChecker : MonoBehaviour
{
    private void OnMouseOver()
    {
        Debug.Log("mouse over: " + gameObject);
    }
}
