using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private Vector3 levelViewPos = new Vector3(-22f, 8.5f, 331f);
    private bool isCameraToMove = true; //todo change back to false when not needed for testing
    [SerializeField] private float cameraMoveSpeed = 200f; // todo change back to 20 once printing Animations have been refactored

    private void Awake()
    {
        GameObject[] sceneManagers = GameObject.FindGameObjectsWithTag("SceneManager");
        if (sceneManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
        if (levelIndex > 0)
        {
            isCameraToMove = true;
        }
    }

    private void Update()
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
}
