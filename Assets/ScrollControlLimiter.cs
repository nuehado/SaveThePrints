using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollControlLimiter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float minDragDistance = 1f;
    private Vector3 previousMousePosition;
    private float yDraggedDistance = 0f;
    private ScrollRect scrollRect;
    private float scrollStepDistance = 0f;
    private float scrollVerticalNormalizedPosition = 1f;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollStepDistance = 1f / (GetComponentsInChildren<Button>().Length - 4);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.vertical = false;
        previousMousePosition = Input.mousePosition;
        scrollVerticalNormalizedPosition = scrollRect.verticalNormalizedPosition;
    }

    public void OnDrag(PointerEventData data)
    {
        Vector3 newMousePosition = Input.mousePosition;
        yDraggedDistance = newMousePosition.y - previousMousePosition.y;

        if (yDraggedDistance >= minDragDistance)
        {
            scrollVerticalNormalizedPosition += scrollStepDistance;
            if (scrollVerticalNormalizedPosition >= 1f)
            {
                scrollVerticalNormalizedPosition = 1f;

            }
            UpdateScrollPosition();
        }

        else if (yDraggedDistance <= -minDragDistance)
        {
            scrollVerticalNormalizedPosition -= scrollStepDistance;
            if (scrollVerticalNormalizedPosition <= 0f)
            {
                scrollVerticalNormalizedPosition = 0f;
            }
            UpdateScrollPosition();
        }
        previousMousePosition = newMousePosition;
    }

    private void UpdateScrollPosition()
    {
        scrollRect.verticalNormalizedPosition = scrollVerticalNormalizedPosition;
        yDraggedDistance = 0f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.vertical = true;
        yDraggedDistance = 0f;
    }

}
