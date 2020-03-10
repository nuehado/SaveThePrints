using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public Button currentSelectedButton;


    public void PressSelectedButton()
    {
        if (currentSelectedButton != null)
        {
            currentSelectedButton.onClick.Invoke();
            Debug.Log("pressing selected button");
        }
        else
        {
            Debug.Log("select button is null");
        }
    }
}
