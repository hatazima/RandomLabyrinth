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

            switch(WorldCreate.type)
            {
                case 0:
                    FadeManager.Instance.LoadScene("Boss");
                    break;
                case 1:
                    FadeManager.Instance.LoadScene("Boss2");
                    break;
                case 2:
                    FadeManager.Instance.LoadScene("Boss3");
                    break;
                case 3:
                    FadeManager.Instance.LoadScene("Boss4");
                    break;
                case 4:
                    FadeManager.Instance.LoadScene("Boss5");
                    break;
                case 5:
                    FadeManager.Instance.LoadScene("Boss6");
                    break;
            }

            GameObject time = FindObjectOfType<TimeCount>().gameObject;
            time.GetComponent<TimeCount>().NextStage();
        }
    }
}
