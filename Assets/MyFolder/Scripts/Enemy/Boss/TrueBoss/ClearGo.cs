using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearGo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
