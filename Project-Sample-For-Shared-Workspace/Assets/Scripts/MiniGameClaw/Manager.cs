using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{

    public TMP_Text TextReward;

    // Start is called before the first frame update
    void Start()
    {
        int JackpotIndex = Random.Range(500, 10000);
        int roundedJackpot = Mathf.RoundToInt(JackpotIndex / 500f) * 500;

        //  int accuracyIndex = Random.Range(60, 90);
        //accuracy increase by 5 by about every 1500 jackpot increment
        int accuracyIndex = Mathf.RoundToInt(Mathf.Lerp(60, 90, Mathf.InverseLerp(500, 10000, JackpotIndex)));
        int roundedAccuracy = Mathf.RoundToInt(accuracyIndex / 5f) * 5;

        //int timeIndex = Random.Range(10, 45);
        int timeIndex = Mathf.RoundToInt(Mathf.Lerp(15, 60, Mathf.InverseLerp(500, 10000, JackpotIndex)));
        int roundedTime = Mathf.RoundToInt(timeIndex / 5f) * 5;


        TextReward.text = "Your Jackpot is: " + roundedJackpot + "\n Accuracy is: " + roundedAccuracy + "\n Target Time is: " + roundedTime;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
