using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGo : MonoBehaviour
{
    public void TitleGoButton()
    {
        //BGMフェードアウト
        AudioManager.Instance.FadeOutBGM();
        //Titleシーンに移動
        FadeManager.Instance.LoadScene("Title");
    }

    void Start()
    {
        if (PlayerData.playerDie)
        {
            //BGMフェードアウト
            AudioManager.Instance.FadeOutBGM();
            //BGM再生
            AudioManager.Instance.PlayBGM("GameOver");
        }
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //BGMフェードアウト
            AudioManager.Instance.FadeOutBGM();
            //Titleシーンに移動
            FadeManager.Instance.LoadScene("Title");
        }
    }
}
