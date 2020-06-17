using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WinPointData
{
    public int winPoints;
    public int purchaseUnlockCost;

    public WinPointData (WinPointCounter winPointCounter)
    {
        winPoints = winPointCounter.winPoints;
        purchaseUnlockCost = winPointCounter.purchaseUnlockCost;
    }
}
