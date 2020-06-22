using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool isMenuOpen = false;
    [SerializeField] private GameObject mainMenuCanvas;
    private LoadManager loadManager;
    [SerializeField] public List<GameObject> winChipSprites = new List<GameObject>();
    public List<MainMenuButton> mainMenuButtons = new List<MainMenuButton>();
    public List<MenuButton> inGameMenuButtons = new List<MenuButton>();


    private void Awake()
    {
        foreach (MainMenuButton menuButton in GetComponentsInChildren<MainMenuButton>())
        {
            mainMenuButtons.Add(menuButton);
        }
        loadManager = FindObjectOfType<LoadManager>();
        GetComponentInChildren<CanvasRenderer>().gameObject.SetActive(false);
        foreach (GameObject inGameButton in loadManager.levelButtons)
        {
            inGameMenuButtons.Add(inGameButton.GetComponent<MenuButton>());
        }
        GetPerLevelPPStatus();
    }
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpen = !isMenuOpen;
            if (isMenuOpen == true)
            {
                GetPerLevelPPStatus();
                mainMenuCanvas.SetActive(true);
            }
            else
            {
                mainMenuCanvas.SetActive(false);
            }
        }        
    }

    private void GetPerLevelPPStatus()
    {
        for(int i = 0; i < mainMenuButtons.Count; i++)
        {
            if( i < loadManager.levelScores.Count )
            {
                Debug.Log("should do the thing");
                mainMenuButtons[i].printPoints = loadManager.levelScores[i];
                if(mainMenuButtons[i].printPoints > 0)
                {
                    mainMenuButtons[i].isLocked = false;
                    mainMenuButtons[i].SetText();
                }
                mainMenuButtons[i].text = inGameMenuButtons[i].text;
            }
        }
    }
}
