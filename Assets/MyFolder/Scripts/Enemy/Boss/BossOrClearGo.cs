using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOrClearGo : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && SelectStage.bossAppearance)
        {
            GameObject time = FindObjectOfType<TimeCount>().gameObject;
            time.GetComponent<TimeCount>().NextStage();
            AudioManager.Instance.FadeOutBGM();
            FadeManager.Instance.LoadScene("TrueBoss");
        }
        else if(other.gameObject.CompareTag("Player") && SelectStage.bossAppearance == false)
        {
            GameObject time = FindObjectOfType<TimeCount>().gameObject;
            time.GetComponent<TimeCount>().NextStage();
            FadeManager.Instance.LoadScene("Clear");
        }
    }

    void Update()
    {
        transform.Rotate(0, 1, 0);
    }
}
