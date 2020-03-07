using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBColorChanger : MonoBehaviour
{
    [SerializeField] GameObject primaryMaterialReferenceObject;
    [SerializeField] GameObject secondaryMaterialReferenceObject;
    private float[] primaryMaterialInitialColor = { 1f, 0f, 0f };
    private float[] secondaryMaterialInitialColor = { 1f, 1f, 0f };

    private List<float> primaryRGB = new List<float>();
    private List<float> secondaryRGB = new List<float>();
    [SerializeField] private Slider sliderPR;
    [SerializeField] private Slider sliderPG;
    [SerializeField] private Slider sliderPB;
    [SerializeField] private Slider sliderSR;
    [SerializeField] private Slider sliderSG;
    [SerializeField] private Slider sliderSB;

    private void Start()
    {
        primaryMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(primaryMaterialInitialColor[0], primaryMaterialInitialColor[1], primaryMaterialInitialColor[2]);
        secondaryMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(secondaryMaterialInitialColor[0], secondaryMaterialInitialColor[1], secondaryMaterialInitialColor[2]);
        primaryRGB.Add(1);
        primaryRGB.Add(0);
        primaryRGB.Add(0);
        
        secondaryRGB.Add(1);
        secondaryRGB.Add(1);
        secondaryRGB.Add(0);
    }


    public void PrimaryRGBChanger(int colorToChange)

    {
        switch (colorToChange)
        {
            case 0: //Primary RGB
                primaryRGB[0] = sliderPR.value;
                primaryRGB[1] = sliderPG.value;
                primaryRGB[2] = sliderPB.value;
                primaryMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(primaryRGB[0], primaryRGB[1], primaryRGB[2]);
                break;

            case 1: //Secondary RGB
                secondaryRGB[0] = sliderSR.value;
                secondaryRGB[1] = sliderSG.value;
                secondaryRGB[2] = sliderSB.value;
                secondaryMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(secondaryRGB[0], secondaryRGB[1], secondaryRGB[2]);
                break;

            default: // no level selected, switch to main menu

                break;
        }
        
    }
}

