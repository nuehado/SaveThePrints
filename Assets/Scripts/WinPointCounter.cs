using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPointCounter : MonoBehaviour
{
    public int winPoints = 0;

    public void AddWinPoints(int playerHealthonWin)
    {
        winPoints += playerHealthonWin;
    }
}
