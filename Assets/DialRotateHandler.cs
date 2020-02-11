using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRotateHandler : MonoBehaviour
{
    //NOTE: need to set manual vetical up/down rotate dial calls for top and bottom button of list
    
    [SerializeField] private Transform rotateOrigin;
    [SerializeField] private float timeBetweenRotations = 0.5f;
    [SerializeField] private SelectedButtonScrollController selectedButtonScrollController;
    private float timer = 0f;
    private Vector3 mouseOffset;
    private float mouseZCoordinate;

    
    private int currentlySelectedButtonIndex;
    private MenuButton[] buttons;
    private bool isRotatingDialManually = false;

    private void OnMouseDrag()
    {
        mouseZCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseOffset = GetMouseAsWorldPoint();
        Vector3 mouseToCenterLine = mouseOffset - rotateOrigin.transform.position;
        Vector3 thisToCenterLine = transform.position - rotateOrigin.transform.position;
        float rotateAngle = Vector3.SignedAngle(mouseToCenterLine, thisToCenterLine, rotateOrigin.up);
        HandleDialTurn(rotateAngle);
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void HandleDialTurn(float rotateAngle)
    {

        if (rotateAngle <= -18f && rotateAngle > -120f)
        {
            SelectMenuButton(1);
            isRotatingDialManually = true;
            CheckIfDialTransformRotateNeeded(1);
            rotateAngle = 0f;
            isRotatingDialManually = false;
        }
        else if (rotateAngle >= 18f && rotateAngle < 120f)
        {
            SelectMenuButton(-1);
            isRotatingDialManually = true;
            CheckIfDialTransformRotateNeeded(-1);
            rotateAngle = 0f;
            isRotatingDialManually = false;
        }
        else
        {
            currentlySelectedButtonIndex = selectedButtonScrollController.currentlySelectedButtonIndex;
            buttons = selectedButtonScrollController.buttons;
            buttons[currentlySelectedButtonIndex].SelectButton();
        }
    }

    public void CheckIfDialTransformRotateNeeded(int rotateSteps)
    {
        Debug.Log("rotate steps " + rotateSteps);
        if (isRotatingDialManually)
        {
            RotateDialTransform(rotateSteps);
            return;
        }

        else if (rotateSteps != 0)
        {
            RotateDialTransform(rotateSteps);
        }
        
    }

    private void RotateDialTransform(int upOrDown)
    {
        rotateOrigin.transform.RotateAround(rotateOrigin.position, rotateOrigin.up, 18f * upOrDown);
    }

    private void SelectMenuButton(int upOrDown)
    {
        currentlySelectedButtonIndex = selectedButtonScrollController.currentlySelectedButtonIndex;
        buttons = selectedButtonScrollController.buttons;
        if (upOrDown < 0 && currentlySelectedButtonIndex < buttons.Length - 1)
        {
            int newButtonSelectionIndex = currentlySelectedButtonIndex + 1;
            buttons[newButtonSelectionIndex].SelectButton();
        }
        else if (upOrDown > 0 && currentlySelectedButtonIndex > 0)
        {
            int newButtonSelectionIndex = currentlySelectedButtonIndex - 1;
            buttons[newButtonSelectionIndex].SelectButton();
        }
    }
}
