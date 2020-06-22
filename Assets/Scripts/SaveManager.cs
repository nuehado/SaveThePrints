using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public WinPointCounter winPointCounter;
    public DefensesStore defensesStore;
    public LoadManager loadManager;

    private void Start()
    {
        winPointCounter = FindObjectOfType<WinPointCounter>();
        defensesStore = FindObjectOfType<DefensesStore>();
        loadManager = FindObjectOfType<LoadManager>();
    }

    public void SaveWinPoints()
    {
        SaveSystem.SaveWinPoints(winPointCounter);
    }

    public void LoadWinPoints()
    {
        WinPointData winPointData = SaveSystem.LoadWinPoints();
        winPointCounter.winPoints = winPointData.winPoints;
        winPointCounter.purchaseUnlockCost = winPointData.purchaseUnlockCost;
        winPointCounter.UpdateWinTrackers();
    }
    
    public void SaveDefenseStore()
    {
        SaveSystem.SaveDefenseStore(defensesStore);
    }
    public void LoadDefenseStore()
    {
        DefensesStoreData defensesStoreData = SaveSystem.LoadDefensesStore();
        int maxTowers = defensesStore.purchasableTowers.Count - 1;
        int maxSupports = defensesStore.purchasableSupports.Count - 1;
        int maxGlueSticks = defensesStore.purchasableGlueSticks.Count - 1;

        for (int i = maxTowers ; i >= 0; i --)
        {
            if(i > defensesStoreData.purchasableTowersSavedIndex)
            {
                defensesStore.purchasedDefenses.Add(defensesStore.purchasableTowers[0]);
                defensesStore.purchasableTowers.Remove(defensesStore.purchasableTowers[0]);
            }
        }
        for (int i = maxSupports; i >= 0; i--)
        {
            if (i > defensesStoreData.purchasableSupportsSavedIndex)
            {
                defensesStore.purchasedDefenses.Add(defensesStore.purchasableSupports[0]);
                defensesStore.purchasableSupports.Remove(defensesStore.purchasableSupports[0]);
            }
        }
        for (int i = maxGlueSticks; i >= 0; i--)
        {
            if (i > defensesStoreData.purchasableGlueStickSavedIndex)
            {
                defensesStore.purchasedDefenses.Add(defensesStore.purchasableGlueSticks[0]);
                defensesStore.purchasableGlueSticks.Remove(defensesStore.purchasableGlueSticks[0]);
            }
        }
    }

    public void SaveLoadManager()
    {
        SaveSystem.SaveLoadManager(loadManager);
    }
    public void LoadLoadManager()
    {
        LoadManagerData loadManagerData = SaveSystem.LoadLoadManager();
        for(int i = 0; i <= loadManagerData.highestLevelUnlocked; i++)
        {
            loadManager.levelButtons[i].GetComponent<Button>().interactable = true;
            loadManager.levelScores[i] = loadManagerData.levelScores[i];
        }
    }
}
