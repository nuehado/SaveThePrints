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
    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<ScrollRect>().vertical = false;
        previousMousePosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData data)
    {
        Vector3 newMousePosition = Input.mousePosition;
        yDraggedDistance = newMousePosition.y - previousMousePosition.y;
        previousMousePosition = newMousePosition;
        if (yDraggedDistance >= minDragDistance)
        {
            //scroll the ScrollRect
            Debug.Log("scroll the scrollrect up");
            yDraggedDistance = 0f;
        }
        if (yDraggedDistance <= -minDragDistance)
        {
            //scroll the ScrollRect
            Debug.Log("scroll the scrollrect down");
            yDraggedDistance = 0f;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<ScrollRect>().vertical = true;
        yDraggedDistance = 0f;
    }

}
