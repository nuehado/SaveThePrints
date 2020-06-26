using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField] private RectTransform credits;
    private float creditsY;
    private float creditsStartHeight;
    private bool isScrolling = false;
    [SerializeField] private float scrollSpeed = 10f;


    private void Awake()
    {
        creditsY = credits.transform.position.y;
        creditsStartHeight = credits.transform.position.y;
    }

    private void OnEnable()
    {
        isScrolling = true;
    }

    void Update()
    {
        if(isScrolling == true)
        {
            creditsY = credits.transform.position.y + scrollSpeed * Time.deltaTime;
            credits.transform.position = new Vector3(credits.transform.position.x, creditsY, credits.transform.position.z);
        }
    }

    private void OnDisable()
    {
        isScrolling = false;
        creditsY = creditsStartHeight;
    }
}
