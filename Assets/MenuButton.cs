using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, ISelectHandler, IMoveHandler
{
    [SerializeField] private string text = "Button Text";
    [SerializeField] private bool isTopButtonInMenu = false;
    [SerializeField] private bool isBottomButtonInMenu = false;
    private SelectedButtonScrollController selectedButtonScrollController;
    private DialRotateHandler dialRotateHandler;

    private void Start()
    {
        SetText();
        selectedButtonScrollController = GetComponentInParent<SelectedButtonScrollController>();
        dialRotateHandler = FindObjectOfType<DialRotateHandler>();
    }

    public void OnMove(AxisEventData eventData)
    {
        if (isTopButtonInMenu == true && eventData.moveVector.y > 0)
        {
            dialRotateHandler.CheckIfDialTransformRotateNeeded(+1);
        }
        if (isBottomButtonInMenu == true && eventData.moveVector.y < 0)
        {
            dialRotateHandler.CheckIfDialTransformRotateNeeded(-1);
        }
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
