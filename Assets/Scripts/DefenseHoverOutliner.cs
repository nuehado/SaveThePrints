using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseHoverOutliner : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeDefenses = new List<GameObject>();

    private Ray ray;
    private GameObject selectedObject;
    private GameObject previousObject;

    void Update()
    {
        if (Input.GetMouseButton(0) != true)
        {
            HoverOverDefense();
        }
    }

    private void HoverOverDefense()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500.0f))
        {
            if (activeDefenses.Contains(hit.transform.gameObject))
            {
                selectedObject = hit.transform.gameObject;
                if (previousObject != selectedObject)
                {
                    if (selectedObject.TryGetComponent<DefenseSupportMover>(out DefenseSupportMover defenseMover))
                    {
                        if (defenseMover.isSupportPlaced == true)
                        {
                            return;
                        }
                    }
                    selectedObject.GetComponent<Outline>().enabled = true;
                    if (previousObject != null)
                    {
                        UnhoverPrevious();
                    }
                }
                previousObject = selectedObject;
            }
            else if (previousObject != null )
            {
                previousObject.GetComponent<Outline>().enabled = false;
                previousObject = null;
            }
        }
        else if (previousObject != null)
        {
            previousObject.GetComponent<Outline>().enabled = false;
            previousObject = null;
        }
    }

    private void UnhoverPrevious()
    {
        if (previousObject != selectedObject)
        {
            previousObject.GetComponent<Outline>().enabled = false;
        }
    }
}
