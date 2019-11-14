using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Appearance : MonoBehaviour
{
    public GameObject wall;
    public GameObject boss4;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wall.SetActive(true);
            boss4.SetActive(true);
            AudioManager.Instance.PlayBGM("Boss4");
            Destroy(gameObject);
        }
    }
}
