using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrophyAnim : MonoBehaviour
{
    [SerializeField] private GameObject trophy;
    [SerializeField] private Canvas winMenu;
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private LoadManager loadManager;
    private AudioSource winSFX;
    private WinPointCounter winPointCounter;
    [SerializeField] private Transform cameraViewPosition;

    [SerializeField] private bool isAnimPlaying = false;
    private Vector3 moveVelocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0.1f;
    private bool isRotationStarted = false;
    private Vector3 originalTrophyPosition;
    private Quaternion originalTrophyRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        winSFX = GetComponent<AudioSource>();
        winPointCounter = FindObjectOfType<WinPointCounter>();
        originalTrophyPosition = this.transform.position;
        originalTrophyRotation = this.transform.rotation;
    }

    private void Update()
    {
        
        if (isAnimPlaying == true)
        {
            float distanceLeftToTravel = Vector3.Distance(trophy.transform.position, cameraViewPosition.position);
            if (distanceLeftToTravel <= 300f && isRotationStarted == false)
            {
                GetComponent<Animator>().SetTrigger("Win");
                isRotationStarted = true;
            }
            if (distanceLeftToTravel <= 1f)
            {
                Debug.Log("Trophy moved");
                isAnimPlaying = false;
                return;
            }
            WinGoalAnim();
        }
    }

    public void WinAnimStart()
    {
        winSFX.Play();
        isAnimPlaying = true;
    }

    private void WinGoalAnim()
    {
        this.transform.position = Vector3.SmoothDamp(transform.position, cameraViewPosition.position, ref moveVelocity, smoothTime);

    }

    private void WinAnimEnd()
    {
        Debug.Log("end called");
        isRotationStarted = false;
        //trophy.SetActive(true);
        winMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        this.transform.position = originalTrophyPosition;
        this.transform.rotation = originalTrophyRotation;
        if (winPointCounter.winPoints >= winPointCounter.purchaseUnlockCost && winPointCounter.winPoints < winPointCounter.purchaseUnlockMax)
        {
            winPointCounter.purchaseUnlockCost += winPointCounter.purchaseUnlockScaler;
            loadManager.ChangeLevel(-4);
        }
        else
        {
            loadManager.ChangeLevel(0);
        }
    }
}
