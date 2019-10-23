using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageGo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //BGMフェードアウト
            AudioManager.Instance.FadeOutBGM();
            //Labyrinthシーンに移動
            FadeManager.Instance.LoadScene("Boss6");
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
