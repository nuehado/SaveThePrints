using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLoader : MonoBehaviour
{
    private Vector3 levelViewPos = new Vector3(-22f, 8.5f, 331f);
    private bool isCameraToMove;
    [SerializeField] private float cameraMoveSpeed = 200f; // todo change back to 20 once printing Animations have been refactored

    private int levelLoaded = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PositionCamera();
    }

    private void PositionCamera()
    {
        if (isCameraToMove == true)
        {
            Camera.main.transform.localPosition = Vector3.MoveTowards(Camera.main.transform.localPosition, levelViewPos, cameraMoveSpeed * Time.deltaTime);
        }

        float cameraMoveDistanceLeft = Vector3.Distance(Camera.main.transform.localPosition, levelViewPos);
        if (cameraMoveDistanceLeft < 0.001)
        {
            isCameraToMove = false;
        }
    }

    private void ChangeLevel()
    {
        switch (levelLoaded)
        {
            case 0: //main menu
                Debug.Log("Main Menu selected");
                break;

            case 1: //level 1
                Debug.Log("Level 1 selected");
                break;

            case 2: //level 2
                Debug.Log("level 2 selected");
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
