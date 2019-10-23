using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HusumaOpen : MonoBehaviour
{
    public GameObject husumaRight;
    public GameObject husumaLeft;
    bool right = false;
    bool left = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            right = true;
            left = true;
            AudioManager.Instance.PlaySE("Taiko");
            Destroy(gameObject, 3);
        }
    }

    void Update()
    {
        if(right)
        {
            husumaRight.transform.Translate(0, 0, 0.05f);
        }
        if(left)
        {
            husumaLeft.transform.Translate(0, 0, -0.05f);
        }
    }
}
