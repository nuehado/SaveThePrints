using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuteMusic : MonoBehaviour
{
    private bool isMuted = false;
    

    private void Awake()
    {
        GameObject[] musicPlayers = GameObject.FindGameObjectsWithTag("music");
        if (musicPlayers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

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
