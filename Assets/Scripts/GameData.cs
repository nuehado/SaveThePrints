using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //General
    public int winPoints;
    public int purchaseUnlockCost;

    //UnlockedTowers

    //Levels

    public GameData (WinPointCounter winPointCounter)
    {
        winPoints = winPointCounter.winPoints;
        purchaseUnlockCost = winPointCounter.purchaseUnlockCost;
    }
}
