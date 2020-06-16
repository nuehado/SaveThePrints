using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XButton : MonoBehaviour
{
    [SerializeField] Canvas printingMenu;
    [SerializeField] Canvas loadingMenu;
    [SerializeField] Canvas quitMenu;
    [SerializeField] Canvas loseMenu;
    [SerializeField] Canvas winMenu;
    [SerializeField] Canvas mainMenu;
    [SerializeField] LoadManager loadManager;
    [SerializeField] Canvas[] allNonPrintMenus;
    [SerializeField] GameObject[] printColorObjects;
    private Canvas previousMenu;
    public void Xbutton()
    {
        if (printingMenu.isActiveAndEnabled == true) //todo add win and lose menu navigations
        {
            previousMenu = printingMenu;
            loadManager.QuitLevel();
        }

        else if (loadingMenu.isActiveAndEnabled == true)
        {
            return;
        }

        else if (quitMenu.isActiveAndEnabled == true)
        {
            loadManager.LoseLevel();
            quitMenu.gameObject.SetActive(false);
        }

        else if (loseMenu.isActiveAndEnabled == true)
        {
            mainMenu.gameObject.SetActive(true);
            loseMenu.gameObject.SetActive(false);
        }

        else if (winMenu.isActiveAndEnabled == true)
        {
            winMenu.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
            loadManager.ChangeLevel(0);
        }

        else
        {
            foreach (Canvas canvas in allNonPrintMenus)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (GameObject colorObject in printColorObjects)
            {
                colorObject.SetActive(false);
            }
            mainMenu.gameObject.SetActive(true);
        }
    }

    public void ReactivatePreviousMenu()
    {
        printingMenu.gameObject.SetActive(true);
        quitMenu.gameObject.SetActive(false);
        loadManager.SetViewPos(false);
    }
}
