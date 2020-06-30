using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGraphics : MonoBehaviour
{
    private int qualityIndex = 2;
    [SerializeField] Sprite[] qualityButtonSprites;
    private Image graphicsButtonImage;
    // Start is called before the first frame update
    void Start()
    {
        graphicsButtonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeQuality()
    {
        if (qualityIndex >= 2)
        {
            qualityIndex = 0;
        }
        else
        {
            qualityIndex++;
        }
        
        QualitySettings.SetQualityLevel(qualityIndex);
        graphicsButtonImage.sprite = qualityButtonSprites[qualityIndex];
    }
}
