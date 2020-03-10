using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBColorChanger : MonoBehaviour
{
    [SerializeField] GameObject printMaterialReferenceObject;
    [SerializeField] GameObject defensesMaterialReferenceObject;
    [SerializeField] GameObject supportMaterialReferenceObject;
    [SerializeField] GameObject lineRendererReferenceObject;
    private float[] printMaterialInitialColor = { 0.77f, 0f, 0f };
    private float[] defensesMaterialInitialColor = { 0f, 0f, 0.77f };
    private float[] supportMaterialInitialColor = { 0.38f, 0.38f, 0.38f };

    private List<float> printRGB = new List<float>();
    private List<float> defensesRGB = new List<float>();
    private List<float> supportRGB = new List<float>();
    [SerializeField] private Slider sliderPR;
    [SerializeField] private Slider sliderPG;
    [SerializeField] private Slider sliderPB;
    [SerializeField] private Slider sliderDR;
    [SerializeField] private Slider sliderDG;
    [SerializeField] private Slider sliderDB;
    [SerializeField] private Slider sliderSR;
    [SerializeField] private Slider sliderSG;
    [SerializeField] private Slider sliderSB;

    private void Start()
    {
        printMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(printMaterialInitialColor[0], printMaterialInitialColor[1], printMaterialInitialColor[2]);
        lineRendererReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(printMaterialInitialColor[0], printMaterialInitialColor[1], printMaterialInitialColor[2]);
        defensesMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(defensesMaterialInitialColor[0], defensesMaterialInitialColor[1], defensesMaterialInitialColor[2]);
        supportMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(supportMaterialInitialColor[0], supportMaterialInitialColor[1], supportMaterialInitialColor[2]);
        printRGB.Add(0.9f);
        printRGB.Add(0f);
        printRGB.Add(0f);
        
        defensesRGB.Add(0.9f);
        defensesRGB.Add(0.9f);
        defensesRGB.Add(0f);

        supportRGB.Add(0.2f);
        supportRGB.Add(0.2f);
        supportRGB.Add(0.2f);
    }


    public void PrimaryRGBChanger(int colorToChange)

    {
        switch (colorToChange)
        {
            case 0: //Print RGB
                printRGB[0] = sliderPR.value / 13f;
                printRGB[1] = sliderPG.value / 13f;
                printRGB[2] = sliderPB.value / 13f;
                printMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(printRGB[0], printRGB[1], printRGB[2]);
                lineRendererReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(printRGB[0], printRGB[1], printRGB[2]);
                break;

            case 1: //Defenses RGB
                defensesRGB[0] = sliderDR.value / 13f;
                defensesRGB[1] = sliderDG.value / 13f;
                defensesRGB[2] = sliderDB.value / 13f;
                defensesMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(defensesRGB[0], defensesRGB[1], defensesRGB[2]);
                break;

            case 2: //Support RGB
                supportRGB[0] = sliderSR.value / 13f;
                supportRGB[1] = sliderSG.value / 13f;
                supportRGB[2] = sliderSB.value / 13f;
                supportMaterialReferenceObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(supportRGB[0], supportRGB[1], supportRGB[2]);
                break;

            default: // no material selected, exit

                break;
        }
        
    }
}

