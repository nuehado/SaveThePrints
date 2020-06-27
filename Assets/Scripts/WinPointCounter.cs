using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinPointCounter : MonoBehaviour
{
    public int winPoints = 0;

    public int purchaseUnlockCost = 12;
    public int purchaseUnlockMax = 84;
    public int purchaseUnlockScaler = 12;

    [SerializeField] List<GameObject> winChips = new List<GameObject>();
    [SerializeField] TextMeshProUGUI winChipCounter;
    [SerializeField] AudioSource boomBaby;
    [SerializeField] GameObject boomButton;

    private void Start()
    {
        winChipCounter.text = winPoints.ToString() + "/" + purchaseUnlockCost.ToString();
    }

    public void AddWinPoints(int playerHealthonWin)
    {
        winPoints += playerHealthonWin;
        if (winPoints >= purchaseUnlockMax)
        {
            boomButton.SetActive(true);
            boomBaby.Play();
        }
    }

    public void UpdateWinTrackers()
    {
        for (int i = 0; i < winPoints && i < winChips.Count; i++)
        {
            winChips[i].SetActive(true);
        }
        winChipCounter.text = winPoints.ToString() + "/" + purchaseUnlockCost.ToString() ;

    }
}
