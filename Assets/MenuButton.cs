using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IDeselectHandler
{
    [SerializeField] private string text = "Button Text";

    private void Start()
    {
        SetText();
    }

    public void SetText()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = " " + text;
    }
    public void SetSelectedText()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = ">" + text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SelectButton();
    }

    private void SelectButton()
    {
        GetComponent<Button>().Select();
        SetSelectedText();
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        SetText();
    }

}
