using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    float seconds = 0;
    float oldSeconds = 0;
    float minute = 0;
    Text timeText;

    void Start()
    {
        seconds = PlayerData.seconds;
        minute = PlayerData.minute;
        oldSeconds = seconds;
        timeText = GetComponent<Text>();
        timeText.text = minute.ToString("00") + ":" + seconds.ToString("00");
    }
    
    void Update()
    {
        seconds += Time.deltaTime;
        if (seconds >= 60f)
        {
            minute++;
            seconds = seconds - 60;
        }
        //　値が変わった時だけテキストUIを更新
        if ((int)seconds != (int)oldSeconds)
        {
            timeText.text = minute.ToString("00") + ":" + seconds.ToString("00");
            oldSeconds = seconds;
        }
    }

    public void NextStage()
    {
        PlayerData.seconds = seconds;
        PlayerData.minute = minute;
    }
}
