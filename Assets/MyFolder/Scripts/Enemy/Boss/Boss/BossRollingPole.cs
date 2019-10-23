using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRollingPole : MonoBehaviour
{
    int count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            count++;
            GetComponent<ParticleSystem>().Play();
            if (count == 30)
            {
                Destroy(gameObject);
            }
        }
    }
}
