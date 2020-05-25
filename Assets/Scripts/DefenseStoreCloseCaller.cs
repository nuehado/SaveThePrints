using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseStoreCloseCaller : MonoBehaviour
{
    private DefensesStore defensesStore;
    void Start()
    {
        defensesStore = FindObjectOfType<DefensesStore>();
    }

    private void CloseStoreCall()
    {
        defensesStore.CloseStore();
    }

    private void PlayDrawerSFXCall()
    {
        defensesStore.PlayDrawerSFX();
    }

    private void PlayUnlockSFXCall()
    {
        defensesStore.PlayUnlockSFX();
    }
}
