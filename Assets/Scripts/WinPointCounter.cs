using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPointCounter : MonoBehaviour
{
    public int winPoints = 0;

    public int purchaseUnlockCost = 3;
    public int purchaseUnlockScaler = 3;

    public void AddWinPoints(int playerHealthonWin)
    {
        winPoints += playerHealthonWin;
    }
}
