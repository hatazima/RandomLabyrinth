using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private void Start()
    {
        //BGM再生
        AudioManager.Instance.PlayBGM("Title");
    }
    
    public void StartButton()//Startボタンを押したときLabyrinthシーンに移動
    {
        //BGMフェードアウト
        AudioManager.Instance.FadeOutBGM();
        //Labyrinthシーンに移動
        FadeManager.Instance.LoadScene("SelectStage");
    }
    
    public void PuitButton()//Puitボタンを押したときゲーム終了
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }
}
