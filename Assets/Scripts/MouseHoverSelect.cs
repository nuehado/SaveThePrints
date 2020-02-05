using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHoverSelect : MonoBehaviour, IPointerEnterHandler
{
    private RadialButton radialButton;

    private void Start()
    {
        radialButton = FindObjectOfType<RadialButton>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetsButtonStatus();
    }

    private void SetsButtonStatus()
    {
        GetComponent<Button>().Select();

        SyncRadialButtonToMouseSelection();
    }

    private void SyncRadialButtonToMouseSelection()
    {
        int currentMenuPositionIndex = radialButton.menuPositionIndex;
        int currentHighlightedButtonIndex = radialButton.highlightedButtonIndex;
        int i = 0;
        foreach (Button button in radialButton.menuButtons)
        {
            if (this.gameObject.name == button.gameObject.name)
            {
                radialButton.highlightedButtonIndex = i;
                int indexChange = i - currentHighlightedButtonIndex;
                
                if (currentMenuPositionIndex + indexChange <= 3 && currentMenuPositionIndex + indexChange >=0)
                {
                    radialButton.menuPositionIndex += indexChange;
                }
            }
            i++;
        }
    }
}
