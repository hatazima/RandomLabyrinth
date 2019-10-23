using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSceneGo : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FadeManager.Instance.LoadScene("Clear");
        }
    }

    void Update()
    {
        transform.Rotate(0, 1, 0);
    }
}
