using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Appearance : MonoBehaviour
{
    public GameObject wall;
    public GameObject boss3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wall.SetActive(true);
            boss3.SetActive(true);
            AudioManager.Instance.PlayBGM("Boss3");
            Destroy(gameObject);
        }
    }
}
