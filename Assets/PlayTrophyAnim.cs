using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrophyAnim : MonoBehaviour
{
    [SerializeField] private GameObject trophy;
    [SerializeField] Canvas winMenu;
    [SerializeField] Canvas mainMenu;
    [SerializeField] LoadManager loadManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TestFunction()
    {
        trophy.SetActive(true);
        winMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        loadManager.ChangeLevel(0);
    }
}
