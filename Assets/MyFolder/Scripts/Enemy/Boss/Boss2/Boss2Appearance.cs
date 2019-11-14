using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Appearance : MonoBehaviour
{
    public GameObject wall;
    public GameObject boss2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wall.SetActive(true);
            boss2.SetActive(true);
            AudioManager.Instance.PlayBGM("Boss2");
            Destroy(gameObject);
        }
    }
}
