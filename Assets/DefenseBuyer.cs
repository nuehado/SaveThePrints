using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuyer : MonoBehaviour
{

    private Outline outline;
    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnMouseOver()
    {
        Debug.Log("mouse over defense");
        outline.enabled = true;
        outline.OutlineColor = Color.yellow;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    private void HoverOverDefense()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500.0f))
        {
            if (gameObject == hit.transform.gameObject)
            {
                outline.enabled = true;
                outline.OutlineColor = Color.yellow;
            }
        }
        else
        {
            outline.enabled = false;
        }
    }*/
}
