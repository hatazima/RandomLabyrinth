using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaSplash : MonoBehaviour
{
    public GameObject[] aquaBall;

    void Start()
    {
        GameObject boss5 = FindObjectOfType<Boss5>().gameObject;

        for (int i = 0; i < aquaBall.Length; i++)
        {
            //式紙の進行方向を求める
            Vector3 vec = (boss5.transform.position - aquaBall[i].transform.position).normalized;
            //式紙を発射する
            aquaBall[i].GetComponent<Rigidbody>().velocity = vec * 10f;
        }
    }
    
    void Update()
    {
        
    }
}
