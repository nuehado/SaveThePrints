using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    private GameObject sceneSwitcher;

    private void Start()
    {
        GetComponent<Button>();
        sceneSwitcher = GameObject.Find("SceneSwitcher");
        this.GetComponent<Button>().onClick.AddListener(LoadMainMenu);
    }

    private void OnEnable()
    {

    }

    void LoadMainMenu()
    {
        sceneSwitcher.GetComponent<SceneSwitcher>().LoadLevel(0);
    }
}
