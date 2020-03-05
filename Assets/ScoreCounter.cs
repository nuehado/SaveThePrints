using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firedText;
    private float firedFilamentCM = 0f;
    private float timer = 0f;
    private string nunmberZeros = "000";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            firedFilamentCM += 0.2f;
            //firedFilamentCM = firedFilamentCM * Time.deltaTime;
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
            else if ( scoreDigits == 2)
            {
                nunmberZeros = "0";
            }
            else
            {
                nunmberZeros = "";
            }
            firedText.text = "Defenses Fired " + nunmberZeros + firedFilamentCM.ToString() + "cm";
            timer = 0;
        }
        
    }
}
