using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrophyAnim : MonoBehaviour
{
    [SerializeField] private GameObject trophy;
    [SerializeField] private Canvas winMenu;
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private LoadManager loadManager;
    private AudioSource winSFX;
    
    // Start is called before the first frame update
    void Start()
    {
        winSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TestFunction2()
    {
        winSFX.Play();
    }

    private void TestFunction()
    {
        trophy.SetActive(true);
        winMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        loadManager.ChangeLevel(0);
    }
}
