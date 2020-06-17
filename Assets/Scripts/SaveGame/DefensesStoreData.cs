using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefensesStoreData
{
    public int purchasableTowersSavedIndex;
    public int purchasableSupportsSavedIndex;
    public int purchasableGlueStickSavedIndex;

    public DefensesStoreData(DefensesStore defensesStore)
    {
        purchasableTowersSavedIndex = defensesStore.purchasableTowers.Count-1;
        purchasableSupportsSavedIndex = defensesStore.purchasableSupports.Count - 1;
        purchasableGlueStickSavedIndex = defensesStore.purchasableGlueSticks.Count - 1;
        
    }
}