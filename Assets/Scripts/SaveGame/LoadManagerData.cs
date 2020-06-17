using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoadManagerData
{
    public int[] levelScores;
    public int highestLevelUnlocked;

    public LoadManagerData(LoadManager loadManager)
    {
        highestLevelUnlocked = loadManager.highestLevelUnlock;
        levelScores = new int[loadManager.levelScores.Count];
        for(int i = 0; i < levelScores.Length; i++)
        {
            levelScores[i] = loadManager.levelScores[i];
        }
    }
}