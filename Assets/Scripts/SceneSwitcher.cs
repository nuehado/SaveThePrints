using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private Vector3 levelViewPos = new Vector3(-22f, 8.5f, 331f);
    private bool isCameraToMove;
    [SerializeField] private float cameraMoveSpeed = 0.1f;

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
    }
}
