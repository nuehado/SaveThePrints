﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinPointCounter : MonoBehaviour
{
    public int winPoints = 0;

    public int purchaseUnlockCost = 3;
    public int purchaseUnlockMax = 18;
    public int purchaseUnlockScaler = 3;

    [SerializeField] List<GameObject> winChips = new List<GameObject>();
    [SerializeField] TextMeshProUGUI winChipCounter;

    private void Start()
    {
        winChipCounter.text = winPoints.ToString() + "/" + purchaseUnlockCost.ToString();
    }

    public void AddWinPoints(int playerHealthonWin)
    {
        winPoints += playerHealthonWin;
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
