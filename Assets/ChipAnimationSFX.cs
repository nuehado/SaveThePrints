using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipAnimationSFX : MonoBehaviour
{
    [SerializeField] private AudioSource chip1SFX;
    [SerializeField] private AudioSource chip2SFX;
    [SerializeField] private AudioSource chip3SFX;

    private void PlayChip1()
    {
        if (chip1SFX.gameObject.activeInHierarchy)
        {
            chip1SFX.Play();
        }
    }

    private void PlayChip2()
    {
        if (chip2SFX.gameObject.activeInHierarchy)
        {
            chip2SFX.Play();
        }
    }
    private void PlayChip3()
    {
        if (chip3SFX.gameObject.activeInHierarchy)
        {
            chip3SFX.Play();
        }
    }
}
