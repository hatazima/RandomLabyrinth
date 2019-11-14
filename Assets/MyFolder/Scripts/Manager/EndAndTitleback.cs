using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAndTitleback : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))//いつでもEscでやめる
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
            #endif
        }
        if(Input.GetKeyDown(KeyCode.F12)) //いつでもF12でタイトルへ戻る
        {
            AudioManager.Instance.FadeOutBGM();
            SceneManager.LoadScene("Title");
        }
    }
}
