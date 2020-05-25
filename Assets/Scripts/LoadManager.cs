using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    // todo get main menu camera position for returning to MM
    private Vector3 levelViewPos = new Vector3(-22f, 8.5f, 331f);
    private Vector3 levelViewRot = new Vector3(45f, 180f, 0f);
    private Vector3 menuViewPos = new Vector3(-20f, -160f, 290f);
    private Vector3 menuViewRot = new Vector3(45f, 180f, 0f);
    private Vector3 buyViewPos = new Vector3(44.4f, -39.2f, 205.9f);
    private Vector3 buyViewRot = new Vector3(40.957f, 115.931f, 0f);
    private Vector3 moveViewPos;
    private Vector3 moveViewRot;
    public bool isCameraToMove;
    [SerializeField] private float cameraMoveSpeed = 200f; // todo change back to 20 once printing Animations have been refactored

    private int levelSelected = 0;
    private int lastLoadedLevel = 0;
    private float timeLast;
    private float timeNow;
    private float deltaTime;

    [SerializeField] GameObject level1;
    [SerializeField] GameObject level1Button;
    private int level1Score = 0;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level2Button;
    private int level2Score = 0;
    [SerializeField] GameObject level3;
    [SerializeField] GameObject level3Button;
    private int level3Score = 0;
    [SerializeField] GameObject level4;
    [SerializeField] GameObject level4Button;
    private int level4Score = 0;
    [SerializeField] GameObject level5;
    [SerializeField] GameObject level5Button;
    private int level5Score = 0;
    [SerializeField] GameObject level6;
    [SerializeField] GameObject level6Button;
    private int level6Score = 0;
    [SerializeField] GameObject level7;
    [SerializeField] GameObject level7Button;
    private int level7Score = 0;

    [SerializeField] private List<GameObject> levels = new List<GameObject>();
    [SerializeField] private List<GameObject> levelButtons = new List<GameObject>();
    [SerializeField] private List<int> levelScores = new List<int>();

    TowerMover[] towers;
    DefenseSupportMover[] supports;
    [SerializeField] SlowStickMover slowSticks;


    [SerializeField] Canvas printingMenu;
    [SerializeField] Canvas loseMenu;
    [SerializeField] Canvas winMenu;
    [SerializeField] Canvas quitMenu;
    private EnemySpawner enemySpawner;
    private GameObject currentLevel = null;
    [SerializeField] ScoreCounter scoreCounter;

    [SerializeField] PlayTrophyAnim[] trophies;
    private AudioSource loseSFX;

    private SlowEffect[] slowEffects;

    private WinPointCounter winPointCounter;
    private DefensesStore defensesStore;

    [SerializeField] List<GameObject> winChipDisplays = new List<GameObject>();
    private DefenseHoverOutliner defenseHover;

    // Start is called before the first frame update
    void Start()
    {
        towers = FindObjectsOfType<TowerMover>();
        supports = FindObjectsOfType<DefenseSupportMover>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        loseSFX = GetComponent<AudioSource>();
        slowEffects = FindObjectsOfType<SlowEffect>();
        winPointCounter = FindObjectOfType<WinPointCounter>();
        defensesStore = FindObjectOfType<DefensesStore>();
        defenseHover = FindObjectOfType<DefenseHoverOutliner>();
        moveViewRot = menuViewRot;

        foreach (GameObject level in levels)
        {
            levelScores.Add(0);
        }
    }

    public void SetViewPos(bool isMenuView)
    {
        if ( isMenuView == true)
        {
            moveViewPos = menuViewPos;
        }

        else
        {
            moveViewPos = levelViewPos;
        }

        isCameraToMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraToMove == true)
        {
            PositionCamera(moveViewPos);
        }
    }

    private void PositionCamera(Vector3 moveViewPos)
    {
        timeNow = Time.realtimeSinceStartup;
        deltaTime = timeLast - timeNow;

        Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, moveViewPos, cameraMoveSpeed * Time.deltaTime); //todo either switch back to manually calculated time or remove all manual calculation script lines
        if (moveViewRot != null)
        {
            /*
             * Vector3 targetDirection = moveViewPos - Camera.main.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, cameraMoveSpeed * Time.deltaTime, 0.0f);
            Camera.main.transform.rotation = Quaternion.LookRotation(newDirection);
            */
            Quaternion newRotation = Quaternion.Euler(moveViewRot);
            Camera.main.transform.localRotation = Quaternion.RotateTowards(Camera.main.transform.localRotation, newRotation, cameraMoveSpeed* 0.4f * Time.deltaTime);
        }
        
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
            case -4: //buy menu
                //Debug.Log("Buy Menu forced");
                ResetLevelState();
                defensesStore.enabled = true;
                moveViewPos = buyViewPos;
                moveViewRot = buyViewRot;
                isCameraToMove = true;


                timeLast = Time.realtimeSinceStartup;
                break;

            case -3: //lose menu
                //Debug.Log("Lose Menu selected");
                ResetLevelState();
                printingMenu.gameObject.SetActive(false);
                loseMenu.gameObject.SetActive(true);
                loseSFX.Play();
                moveViewPos = menuViewPos;
                moveViewRot = menuViewRot;
                isCameraToMove = true;


                timeLast = Time.realtimeSinceStartup;
                break;

            case -2: //quit menu
                //Debug.Log("Quit Menu selected");
                moveViewPos = menuViewPos;
                moveViewRot = menuViewRot;
                isCameraToMove = true;

                timeLast = Time.realtimeSinceStartup;
                break;

            case -1: //win menu
                //Debug.Log("Win Menu selected");
                printingMenu.gameObject.SetActive(false);
                quitMenu.gameObject.SetActive(false);
                winMenu.gameObject.SetActive(true);
                currentLevel = GameObject.FindGameObjectWithTag("Level"); // has to happen before level object is disabled
                PlayerHealth playerHealth = currentLevel.GetComponentInChildren<PlayerHealth>();
                int currentLevelScore = playerHealth.playerHealth;
                for (int i = 0; i < currentLevelScore; i++)
                {
                    winChipDisplays[i].SetActive(true);
                }
                if (currentLevelScore > levelScores[lastLoadedLevel - 1])
                {
                    winPointCounter.AddWinPoints(currentLevelScore - levelScores[lastLoadedLevel - 1]);
                    levelScores[lastLoadedLevel - 1] = currentLevelScore;
                }
                //trophies[lastLoadedLevel - 1].GetComponent<Animator>().SetTrigger("Win"); // this is needed to automatically get back to main menu or buy menu
                trophies[lastLoadedLevel - 1].GetComponent<PlayTrophyAnim>().WinAnimStart();
                timeLast = Time.realtimeSinceStartup;
                break;

            case 0: //main menu
                //Debug.Log("Main Menu selected");
                foreach (GameObject winChipDisplay in winChipDisplays)
                {
                    winChipDisplay.SetActive(false);
                }
                winPointCounter.UpdateWinTrackers();
                moveViewPos = menuViewPos;
                moveViewRot = menuViewRot;
                isCameraToMove = true;
                ResetLevelState();
                foreach (TowerMover towerMover in towers)
                {
                    towerMover.ResetTowerToStart();
                    towerMover.enabled = false;
                }
                foreach (DefenseSupportMover supportMover in supports)
                {
                    supportMover.ResetSupportToStart();
                    supportMover.enabled = false;
                }
                foreach(SlowEffect slowArea in slowEffects)
                {
                    slowArea.ResetSlowEffect();
                }
                slowSticks.ResetStickToStart();
                slowSticks.enabled = false;

                timeLast = Time.realtimeSinceStartup;
                // for win scenario we turn on level1, ChangeLevel(1). and switch menus
                break;

            case 1: //level 1
                    //Debug.Log("Level 1 selected");

                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                levels[levelSelected - 1].SetActive(true);
                enemySpawner.ChangeEnemySpawnAmount(5);
                lastLoadedLevel = levelSelected;
                moveViewPos = levelViewPos;
                menuViewRot = levelViewRot;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;

                //set number of towers, defenseSupports, and slowStick. (towers[0].gameObject.SetActive(true);)
                //towers
                ActivateUnlockedDefenses();
                break;

            case 2: //level 2
                //Debug.Log("level 2 selected");
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                levels[levelSelected - 1].SetActive(true);
                enemySpawner.ChangeEnemySpawnAmount(3);
                lastLoadedLevel = levelSelected;
                moveViewPos = levelViewPos;
                menuViewRot = levelViewRot;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;

                //set number of towers, Dsupports, etc.

                ActivateUnlockedDefenses(); 
                //slowSticks.GetComponentInChildren<MeshRenderer>().enabled = true;
                break;

            case 3: //level 3
                //Debug.Log("Level 3 selecteed");
                levels[levelSelected - 1].SetActive(true);
                enemySpawner.ChangeEnemySpawnAmount(5);
                lastLoadedLevel = levelSelected;
                moveViewPos = levelViewPos;
                menuViewRot = levelViewRot;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;

                //set number of towers, defenseSupports, and slowStick. (towers[0].gameObject.SetActive(true);)
                //towers
                ActivateUnlockedDefenses();

                break;

            case 4: //level 4
                //Debug.Log("Level 4 selecteed");
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                level4.SetActive(true);
                lastLoadedLevel = levelSelected;
                moveViewPos = levelViewPos;
                menuViewRot = levelViewRot;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;

                //set number of towers, Dsupports, etc.
                ActivateUnlockedDefenses();
                break;

            case 5: //level 5
                //Debug.Log("Level 5 selecteed");
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                level5.SetActive(true);
                lastLoadedLevel = levelSelected;
                moveViewPos = levelViewPos;
                menuViewRot = levelViewRot;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;

                //set number of towers, Dsupports, etc.
                ActivateUnlockedDefenses();
                break;

            case 6: //level 6
                //Debug.Log("Level 6 selecteed");
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                level6.SetActive(true);
                lastLoadedLevel = levelSelected;
                moveViewPos = levelViewPos;
                menuViewRot = levelViewRot;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;
                ActivateUnlockedDefenses();
                //set number of towers, Dsupports, etc.

                break;

            case 7: //level 7
                //Debug.Log("Level 7 selecteed");
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                level7.SetActive(true);
                lastLoadedLevel = levelSelected;
                moveViewPos = levelViewPos;
                menuViewRot = levelViewRot;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;
                ActivateUnlockedDefenses();
                //set number of towers, Dsupports, etc.

                break;

            default: // no level selected, switch to main menu
                Debug.Log("no level selected switching to main menu");
                break;
        }

        //Debug.Log("lastlevelloaded" + lastLoadedLevel);
    }

    private void ActivateUnlockedDefenses()
    {
        foreach (GameObject allDefenseObject in defensesStore.allDefenses)
        {
            allDefenseObject.SetActive(false);
        }

        foreach(GameObject defenseObject in defensesStore.purchasedDefenses)
        {
            if (defenseObject.tag == "Tower")
            {
                var towerMover = defenseObject.GetComponent<TowerMover>();
                towerMover.enabled = false;
                towerMover.ResetTowerToStart();
                
            }
            if (defenseObject.tag == "DefenseSupport")
            {
                var defenseSupportMover = defenseObject.GetComponent<DefenseSupportMover>();
                defenseSupportMover.enabled = false;
                defenseSupportMover.ResetSupportToStart();
            }
            if (defenseObject.tag == "SlowStick")
            {
                var slowStickMover = defenseObject.GetComponent<SlowStickMover>();
                slowStickMover.enabled = false;
                slowStickMover.ResetStickToStart();
            }
            defenseObject.SetActive(true);
        }
    }

    public void ReplayLevel()
    {
        ChangeLevel(lastLoadedLevel);
    }

    public void LoseLevel()
    {
        ChangeLevel(-3);
    }

    public void WinLevel()
    {
        UnlockNextLevel();
        ChangeLevel(-1);
    }

    public void UnlockNextLevel()
    {
        if(lastLoadedLevel < levelButtons.Count)
        {
            levelButtons[lastLoadedLevel].GetComponent<Button>().interactable = true;
        }
        else
        {
            Debug.Log("all levels unlocked");
        }
        
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
        defenseHover.enabled = false;
        currentLevel = GameObject.FindGameObjectWithTag("Level"); // has to happen before level object is disabled
        if (currentLevel != null)
        {
            PlayerHealth playerHealth = currentLevel.GetComponentInChildren<PlayerHealth>();
            playerHealth.playerHealth = playerHealth.maxPlayerHealth;
            currentLevel.SetActive(false);
        }
        foreach (TowerMover towerMover in towers)
        {
            towerMover.ResetTowerToStart();
            towerMover.enabled = false;
        }
        foreach (DefenseSupportMover supportMover in supports)
        {
            supportMover.GetComponent<Outline>().OutlineColor = Color.white;
            supportMover.ResetSupportToStart();
            supportMover.enabled = false;
        }
        slowSticks.ResetStickToStart();
        slowSticks.enabled = false;
        
    }
}
