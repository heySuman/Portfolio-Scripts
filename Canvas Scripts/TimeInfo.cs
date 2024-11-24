using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeInfo : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    private float timeOnGame = 0f;

    // Update is called once per frame
    void Update()
    {
        timeOnGame += Time.deltaTime;

        int min = Mathf.FloorToInt(timeOnGame/60f);
        int sec = Mathf.FloorToInt(timeOnGame%60f);

        timeDisplay.text = string.Format("{0:00}:{1:00}",min, sec) ;
    }
}
