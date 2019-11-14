using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearStage : MonoBehaviour
{
    int type = 0;               //迷宮のタイプ
    public GameObject typeText; //上記のテキストオブジェクト
    string typeStr;             //上記の文字列変数

    bool bossWin = false;          //TrueBossを撃破したかどうか
    public GameObject bossWinText; //上記のテキストオブジェクト
    string bossWinStr;             //上記の文字列変数

    int horizon = 0;            //迷宮の横の長さ
    int vertical = 0;           //迷宮の縦の長さ
    public GameObject sizeText; //上記二つのテキストオブジェクト

    float seconds = 0;          //秒
    float minute = 0;           //分
    public GameObject timeText; //上記二つのテキストオブジェクト

    float evaluationValue = 0;            //全体評価のための数値
    public GameObject evaluationText;   //上記のテキストオブジェクト
    string evaluationStr = "まだまだ!"; //上記の文字列変数

    void Start()
    {
        // 迷宮のタイプのText表示
        type = WorldCreate.type;
        switch (type)
        {
            case 0: typeStr = "通常"; break;
            case 1: typeStr = "木"; break;
            case 2: typeStr = "石"; break;
            case 3: typeStr = "砂漠"; break;
            case 4: typeStr = "海"; break;
            case 5: typeStr = "和風"; break;
        }
        Text tText = typeText.GetComponent<Text>();
        tText.text = typeStr;

        // TrueBossを撃破したかどうかのText表示
        bossWin = SelectStage.bossAppearance;
        if(bossWin) bossWinStr = "撃破";
        else bossWinStr = "未撃破";
        Text bText = bossWinText.GetComponent<Text>();
        bText.text = bossWinStr;

        // 迷宮の大きさのText表示
        horizon = SelectStage.horizon;
        vertical = SelectStage.vertical;
        Text sText = sizeText.GetComponent<Text>();
        sText.text = horizon + "×" + vertical;

        // 迷宮攻略にかかった時間
        seconds = PlayerData.seconds;
        minute = PlayerData.minute;
        Text tiText = timeText.GetComponent<Text>();
        tiText.text = minute.ToString("00") + ":" + seconds.ToString("00");

        switch(horizon)
        {
            case 3: horizon = 7; break;
            case 4: horizon = 6; break;
            case 5: horizon = 5; break;
            case 6: horizon = 4; break;
            case 7: horizon = 3; break;
        }
        switch(vertical)
        {
            case 3: vertical = 7; break;
            case 4: vertical = 6; break;
            case 5: vertical = 5; break;
            case 6: vertical = 4; break;
            case 7: vertical = 3; break;
        }

        // 迷宮のタイプのText表示
        evaluationValue = ((minute * 60 + seconds) * 0.5f) * (horizon * vertical);
        if (bossWin) evaluationValue *= 0.5f;

        if (evaluationValue <= 2000　&& bossWin) evaluationStr = "★完全制覇★";
        else if (evaluationValue <= 2000) evaluationStr = "これはすごい！";
        else if (evaluationValue <= 3500) evaluationStr = "なかなか！";
        else if (evaluationValue <= 6000) evaluationStr = "まだまだ！";
        else if (evaluationValue <= 8000) evaluationStr = "もう少し上を目指そう！";
        else evaluationStr = "これはひどい";

        
        Text eText = evaluationText.GetComponent<Text>();
        eText.text = evaluationStr;
    }
}
