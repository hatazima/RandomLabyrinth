using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igaguri : MonoBehaviour
{
    public GameObject igaguri;
    public GameObject explosionIgaguri;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = Instantiate(explosionIgaguri, this.transform.position , Quaternion.identity);
        Destroy(obj,1.5f);
        Destroy(gameObject);
    }
}
