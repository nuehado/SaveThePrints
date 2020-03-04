using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLoader : MonoBehaviour
{
    // todo get main menu camera position for returning to MM
    private Vector3 levelViewPos = new Vector3(-22f, 8.5f, 331f);
    private Vector3 menuViewPos = new Vector3(-20f, -160f, 290f);
    private Vector3 moveViewPos;
    public bool isCameraToMove;
    [SerializeField] private float cameraMoveSpeed = 200f; // todo change back to 20 once printing Animations have been refactored

    private int levelSelected = 0;
    private float timeLast;
    private float timeNow;
    private float deltaTime;

    [SerializeField] GameObject level1;
    TowerMover[] towers;

    // Start is called before the first frame update
    void Start()
    {
        towers = FindObjectsOfType<TowerMover>();
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
            case 0: //main menu
                Debug.Log("Main Menu selected");
                moveViewPos = menuViewPos;
                isCameraToMove = true;
                foreach (TowerMover tower in towers)
                {
                    tower.ResetTowerToStart();
                }

                timeLast = Time.realtimeSinceStartup;
                break;

            case 1: //level 1
                Debug.Log("Level 1 selected");
                
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                level1.SetActive(true);
                moveViewPos = levelViewPos;
                isCameraToMove = true;
                timeLast = Time.realtimeSinceStartup;
                break;

            case 2: //level 2
                Debug.Log("level 2 selected");
                isCameraToMove = true;
                break;

            case 3: //level 3
                Debug.Log("Level 3 selecteed");
                break;

            default: // no level selected, switch to main menu
                Debug.Log("no level selected switching to main menu");
                break;
        }
    }
}
