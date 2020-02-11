using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectOverride : ScrollRect, IScrollHandler
{
    // Start is called before the first frame update
    public override void OnScroll(PointerEventData data)
    {
        base.OnScroll(data);

        Debug.Log(data.scrollDelta.y);
    }
}
