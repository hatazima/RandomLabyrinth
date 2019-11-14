using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss5Appearance : MonoBehaviour
{
    public GameObject wall;
    public GameObject boss5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wall.SetActive(true);
            boss5.SetActive(true);
            AudioManager.Instance.PlayBGM("Boss5");
            Destroy(gameObject);
        }
    }
}
