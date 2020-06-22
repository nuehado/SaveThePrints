using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, ISelectHandler
{
    public string text = "Button Text";
    private string lockedText = "unprinted";
    public bool isLocked = true;
    public int printPoints = 0;
    private MainMenu mainMenu;
    private List<GameObject> winChipSprites = new List<GameObject>();


    private void Start()
    {
        mainMenu = FindObjectOfType<MainMenu>();
        winChipSprites = mainMenu.winChipSprites;
        if(isLocked)
        {
            SetLockedText();
        }
        else
        {
            SetText();
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
    private void SetSelectedLockedText()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = "-" + lockedText;
    }
    private void SetLockedText()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = " " + lockedText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Button>().Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (!isLocked)
        {
            SetSelectedText();
            for (int i = 0; i < printPoints; i++)
            {
                winChipSprites[i].SetActive(true);
            }
        }
        else
        {
            SetLockedText();
        }
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        if (!isLocked)
        {
            SetText();
        }
        else
        {
            SetLockedText();
        }

        foreach (GameObject winChipSprite in winChipSprites)
        {
            winChipSprite.SetActive(false);
        }
    }
}
