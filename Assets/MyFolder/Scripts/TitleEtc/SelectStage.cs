using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public static int type { get; private set; } = 6; //迷宮のタイプを決める変数
    public GameObject typeText;                       //上記のテキストオブジェクト
    Text tText;                                       //上記のテキスト
    string typeStr;                                   //上記の文字列変数

    public static bool bossAppearance = false; //ボスを出現させるかを決める変数
    public GameObject[] bossAppearanceText;   //上記のテキストオブジェクト
    
    public static int horizon { get; private set; } = 5;  //迷宮の横の長さを決める変数
    public GameObject horizonText;                        //上記のテキストオブジェクト
    Text hText;                                           //上記のテキスト
    public static int vertical { get; private set; } = 5; //迷宮の縦の長さを決める変数
    public GameObject verticalText;                       //上記のテキストオブジェクト
    Text vText;                                           //上記のテキスト

    public static int enemyCreateCount { get; private set; } = 6; //敵の出現数
    
    public void TypeUpButton()//迷宮のタイプ値を上げるボタン
    {
        if (type < 6)
        {
            type++;
            TypeChange();
        }
        else if (type == 6)
        {
            type = 0;
            TypeChange();
        }
    }
    public void TypeDownButton()//迷宮のタイプ値を下げるボタン
    {
        if (type > 0)
        {
            type--;
            TypeChange();
        }
        else if (type == 0)
        {
            type = 6;
            TypeChange();
        }
    }

    public void YesButton()//迷宮のボスを出現させるボタン
    {
        bossAppearanceText[0].GetComponent<Image>().color = new Color(0, 0, 0);
        bossAppearanceText[1].GetComponent<Image>().color = new Color(1, 1, 1);
        bossAppearance = true;
    }
    public void NoButton()//迷宮のボスを出現させないボタン
    {
        bossAppearanceText[1].GetComponent<Image>().color = new Color(0, 0, 0);
        bossAppearanceText[0].GetComponent<Image>().color = new Color(1, 1, 1);
        bossAppearance = false;
    }

    public void HorizonUpButton()//迷宮の横の長さを上げるボタン
    {
        if (horizon < 7)
        {
            horizon++;
            // テキストの表示を入れ替える
            hText = horizonText.GetComponent<Text>();
            hText.text = "横・" + horizon;
        }
        else if (horizon == 7)
        {
            horizon = 3;
            // テキストの表示を入れ替える
            hText = horizonText.GetComponent<Text>();
            hText.text = "横・" + horizon;
        }
    }
    public void HorizonDownButton()//迷宮の横の長さを下げるボタン
    {
        if (horizon > 3)
        {
            horizon--;
            // テキストの表示を入れ替える
            hText = horizonText.GetComponent<Text>();
            hText.text = "横・" + horizon;
        }
        else if (horizon == 3)
        {
            horizon = 7;
            // テキストの表示を入れ替える
            hText = horizonText.GetComponent<Text>();
            hText.text = "横・" + horizon;
        }
    }
    public void VerticalUpButton()//迷宮の縦の長さを上げるボタン
    {
        if (vertical < 7)
        {
            vertical++;
            // テキストの表示を入れ替える
            vText = verticalText.GetComponent<Text>();
            vText.text = "縦・" + vertical;
        }
        else if (vertical == 7)
        {
            vertical = 3;
            // テキストの表示を入れ替える
            vText = verticalText.GetComponent<Text>();
            vText.text = "縦・" + vertical;
        }
    }
    public void VerticalDownButton()//迷宮の縦の長さを下げるボタン
    {
        if (vertical > 3)
        {
            vertical--;
            // テキストの表示を入れ替える
            vText = verticalText.GetComponent<Text>();
            vText.text = "縦・" + vertical;
        }
        else if (vertical == 3)
        {
            vertical = 7;
            // テキストの表示を入れ替える
            vText = verticalText.GetComponent<Text>();
            vText.text = "縦・" + vertical;
        }
    }

    public void LabyrinthGo()
    {
        //BGMフェードアウト
        AudioManager.Instance.FadeOutBGM();
        //Labyrinthシーンに移動
        FadeManager.Instance.LoadScene("Labyrinth");
    }

    void Start()
    {
        //TrueBossを出現させなくする
        NoButton();

        // 初期テキストの表示
        TypeChange();
        // 初期テキストの表示
        hText = horizonText.GetComponent<Text>();
        hText.text = "横・" + horizon;
        // 初期テキストの表示
        vText = verticalText.GetComponent<Text>();
        vText.text = "縦・" + vertical;
    }
    
    void Update()
    {
        
    }

    void TypeChange()
    {
        switch (type)
        {
            case 0: typeStr = "通常"; break;
            case 1: typeStr = "木"; break;
            case 2: typeStr = "石"; break;
            case 3: typeStr = "砂漠"; break;
            case 4: typeStr = "海"; break;
            case 5: typeStr = "和風"; break;
            case 6: typeStr = "ランダム"; break;
        }
        // テキストの表示を入れ替える
        tText = typeText.GetComponent<Text>();
        tText.text = typeStr;
    }
}
