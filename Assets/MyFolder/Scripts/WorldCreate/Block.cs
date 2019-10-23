using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("BlockBreak"))
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
