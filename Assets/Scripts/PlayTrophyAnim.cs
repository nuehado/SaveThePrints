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
    private WinPointCounter winPointCounter;
    private int purchaseUnlock = 3;
    private int purchaseUnlockScaler = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        winSFX = GetComponent<AudioSource>();
        winPointCounter = FindObjectOfType<WinPointCounter>();
    }

    private void WinAnimStart()
    {
        winSFX.Play();
    }

    private void WinAnimEnd()
    {
        trophy.SetActive(true);
        winMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        if (winPointCounter.winPoints >= purchaseUnlock)
        {
            purchaseUnlock += purchaseUnlockScaler;
            loadManager.ChangeLevel(-4);
        }
        else
        {
            loadManager.ChangeLevel(0);
        }
    }
}
