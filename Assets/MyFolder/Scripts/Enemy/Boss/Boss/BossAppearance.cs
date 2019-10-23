using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearance : MonoBehaviour
{
    public GameObject boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
            AudioManager.Instance.PlayBGM("Boss");
            Destroy(gameObject);
        }
    }
}
