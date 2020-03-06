using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    // todo get main menu camera position for returning to MM
    private Vector3 levelViewPos = new Vector3(-22f, 8.5f, 331f);
    private Vector3 menuViewPos = new Vector3(-20f, -160f, 290f);
    private Vector3 moveViewPos;
    public bool isCameraToMove;
    [SerializeField] private float cameraMoveSpeed = 200f; // todo change back to 20 once printing Animations have been refactored

    private int levelSelected = 0;
    private int lastLoadedLevel = 0;
    private float timeLast;
    private float timeNow;
    private float deltaTime;

    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    TowerMover[] towers;

    [SerializeField] Canvas printingMenu;
    [SerializeField] Canvas loseMenu;
    [SerializeField] Canvas winMenu;
    [SerializeField] Canvas quitMenu;
    private EnemySpawner enemySpawner;
    private GameObject currentLevel = null;
    [SerializeField] ScoreCounter scoreCounter;

    [SerializeField] GameObject[] trophies;
    


    // Start is called before the first frame update
    void Start()
    {
        towers = FindObjectsOfType<TowerMover>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraToMove == true)
        {
            PositionCamera();
        }
    }

    private void PositionCamera()
    {
        timeNow = Time.realtimeSinceStartup;
        deltaTime = timeLast - timeNow;

        Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, moveViewPos, cameraMoveSpeed * -deltaTime);

        float cameraMoveDistanceLeft = Vector3.Distance(Camera.main.transform.localPosition, moveViewPos);
        if (cameraMoveDistanceLeft < 0.001)
        {
            isCameraToMove = false;
        }
        timeLast = timeNow;
    }

    public void ChangeLevel(int levelInt)
    {
        levelSelected = levelInt;
        
        switch (levelSelected)
        {
            case -3: //lose menu
                Debug.Log("Lose Menu selected");
                ResetLevelState();
                printingMenu.gameObject.SetActive(false);
                loseMenu.gameObject.SetActive(true);
                
                moveViewPos = menuViewPos;
                isCameraToMove = true;


                timeLast = Time.realtimeSinceStartup;
                break;

            case -2: //quit menu
                Debug.Log("Quit Menu selected");
                moveViewPos = menuViewPos;
                isCameraToMove = true;

                timeLast = Time.realtimeSinceStartup;
                break;

            case -1: //win menu
                Debug.Log("Win Menu selected");
                printingMenu.gameObject.SetActive(false);
                quitMenu.gameObject.SetActive(false);
                winMenu.gameObject.SetActive(true);
                trophies[lastLoadedLevel - 1].SetActive(true);
                timeLast = Time.realtimeSinceStartup;
                break;

            case 0: //main menu
                Debug.Log("Main Menu selected");
                moveViewPos = menuViewPos;
                isCameraToMove = true;
                ResetLevelState();
                foreach (TowerMover towerMover in towers)
                {
                    towerMover.ResetTowerToStart();
                    towerMover.enabled = false;
                }

                timeLast = Time.realtimeSinceStartup;
                // for win scenario we turn on level1, ChangeLevel(1). and switch menus
                break;

            case 1: //level 1
                Debug.Log("Level 1 selected");
                
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                level1.SetActive(true);
                lastLoadedLevel = levelSelected;
                moveViewPos = levelViewPos;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;
                break;

            case 2: //level 2
                Debug.Log("level 2 selected");
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                level2.SetActive(true);
                lastLoadedLevel = levelSelected;
                moveViewPos = levelViewPos;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;
                break;

            case 3: //level 3
                Debug.Log("Level 3 selecteed");
                break;

            default: // no level selected, switch to main menu
                Debug.Log("no level selected switching to main menu");
                break;
        }

        Debug.Log("lastlevelloaded" + lastLoadedLevel);
    }
    public void ReplayLevel()
    {
        Debug.Log("lastlevel" + lastLoadedLevel);
        ChangeLevel(lastLoadedLevel);
    }

    public void LoseLevel()
    {
        ChangeLevel(-3);
    }
    

    public void WinLevel()
    {
        ChangeLevel(-1);
    }
    public void QuitLevel()
    {
        printingMenu.gameObject.SetActive(false);
        quitMenu.gameObject.SetActive(true);
        ChangeLevel(-2);

    }

    public void ResetLevelState()
    {
        enemySpawner.ClearEnemies();
        enemySpawner.enemyCount = 0;
        enemySpawner.enemyHealth = enemySpawner.maxEnemies;
        scoreCounter.firedFilamentCM = 0;
        scoreCounter.UpdateScore();
        currentLevel = GameObject.FindGameObjectWithTag("Level"); // has to happen before level object is disabled
        PlayerHealth playerHealth = currentLevel.GetComponentInChildren<PlayerHealth>();
        playerHealth.playerHealth = playerHealth.maxPlayerHealth;
        if (currentLevel != null)
        {
            currentLevel.SetActive(false);
        }
        foreach (TowerMover towerMover in towers)
        {
            towerMover.ResetTowerToStart();
            towerMover.enabled = false;
        }
    }
}
