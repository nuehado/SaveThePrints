﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, ISelectHandler
{
    [SerializeField] private string text = "Button Text";
    private SelectedButtonScrollController selectedButtonScrollController;

    private void Start()
    {
        SetText();
        selectedButtonScrollController = GetComponentInParent<SelectedButtonScrollController>();
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
        if (eventData.IsPointerMoving())
        {
            SelectButton();
        }
    }

    public void SelectButton()
    {
        GetComponent<Button>().Select();
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        SetText();
    }

    public void OnSelect(BaseEventData eventData)
    {
        SetSelectedText();
        selectedButtonScrollController.SetMenuPosition(GetComponent<Button>());
    }
}
