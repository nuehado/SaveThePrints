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
    private SelectButton selectButton;

    private void Awake()
    {
        selectButton = FindObjectOfType<SelectButton>();
        if (isTopButtonInMenu == true)
        {
            SetSelectedText();
        }
        else
        {
            SetText();
        }
        selectedButtonScrollController = GetComponentInParent<SelectedButtonScrollController>();
        dialRotateHandler = FindObjectOfType<DialRotateHandler>();
    }

    private void OnEnable()
    {
        if (isTopButtonInMenu == true)
        {
            SetSelectedText();
            SetSelectButton();
        }
        else
        {
            SetText();
        }
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
        //selectButton.currentSelectedButton = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        SetSelectedText();
        selectedButtonScrollController.SetMenuPosition(GetComponent<Button>());
        SetSelectButton();
    }

    private void SetSelectButton()
    {
        selectButton.currentSelectedButton = this.GetComponent<Button>();
        //Debug.Log("setting select button selected button");
    }
}
