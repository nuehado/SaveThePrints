using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firedText;
    public float firedFilamentCM = 0f;
    private float timer = 0f;
    private string nunmberZeros = "000";

    public void UpdateScore()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            firedFilamentCM += 1f;
            //firedFilamentCM = firedFilamentCM * Time.deltaTime;
            
            timer = 0;
        }
        string newScore = firedFilamentCM.ToString();
        int scoreDigits = newScore.Length;
        if (scoreDigits == 0)
        {
            nunmberZeros = "000";
        }
        else if (scoreDigits == 1)
        {
            nunmberZeros = "00";
        }
        else if (scoreDigits == 2)
        {
            nunmberZeros = "0";
        }
        else
        {
            nunmberZeros = "";
        }
        firedText.text = "Defenses Fired " + nunmberZeros + firedFilamentCM.ToString() + "cm";
    }
}
