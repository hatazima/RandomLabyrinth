using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HusumaClose : MonoBehaviour
{
    public GameObject husuma;
    public GameObject boss6;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            husuma.SetActive(true);
            boss6.SetActive(true);
            AudioManager.Instance.PlayBGM("Boss6");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AudioManager.Instance.PlaySE("Boss6");
    }
    
    void Update()
    {
        
    }
}
