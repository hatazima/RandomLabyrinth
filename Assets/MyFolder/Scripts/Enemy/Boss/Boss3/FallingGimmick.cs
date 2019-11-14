using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGimmick : MonoBehaviour
{
    public GameObject fallingGimmick;
    bool fall = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fall = true;
            AudioManager.Instance.PlaySE("Impact");
            Destroy(gameObject, 3);
        }
    }

    void Update()
    {
        if (fall)
        {
            fallingGimmick.transform.Translate(0, -0.1f, 0);
        }
    }
}
