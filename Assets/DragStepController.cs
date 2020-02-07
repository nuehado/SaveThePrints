using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragStepController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float minDragDistance = 1f;
    private Vector3 previousMousePosition;
    private float yDraggedDistance = 0f;
    private ScrollRect scrollRect;
    private ScrollStepHandler scrollStepHandler;

    private int up = 1;
    private int down = -1;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollStepHandler = GetComponent<ScrollStepHandler>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.vertical = false;
        previousMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData data)
    {
        Vector3 newMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        yDraggedDistance = newMousePosition.y - previousMousePosition.y;

        if (yDraggedDistance >= minDragDistance)
        {
            scrollStepHandler.UpdateScrollPosition(up);
            yDraggedDistance = 0f;
            previousMousePosition = newMousePosition;
        }

        else if (yDraggedDistance <= -minDragDistance)
        {
            scrollStepHandler.UpdateScrollPosition(down);
            yDraggedDistance = 0f;
            previousMousePosition = newMousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.vertical = true;
        yDraggedDistance = 0f;
    }
}
