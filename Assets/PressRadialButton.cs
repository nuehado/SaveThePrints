using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressRadialButton : MonoBehaviour
{
    [SerializeField] private Button selectedButton;

    public void ClickSelectedButton()
    {
        selectedButton.onClick.Invoke();
    }
    
}
