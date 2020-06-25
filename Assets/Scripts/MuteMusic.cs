using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour
{
    private bool isMuted = false;
    public void PausePlayBGM()
    {
        isMuted = !isMuted;

        if(isMuted)
        {
            GetComponent<AudioSource>().Pause();
        }
        else
        {
            GetComponent<AudioSource>().UnPause();
        }
        
    }
}
