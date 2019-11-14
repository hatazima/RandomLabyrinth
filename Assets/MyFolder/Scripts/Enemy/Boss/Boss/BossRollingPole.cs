using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRollingPole : MonoBehaviour
{
    int count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            count++;
            GetComponent<ParticleSystem>().Play();
            if (count >= 30)
            {
                Destroy(gameObject);
            }
        }
        else if(other.gameObject.CompareTag("PlayerSword"))
        {
            count += 2;
            GetComponent<ParticleSystem>().Play();
            if (count >= 30)
            {
                Destroy(gameObject);
            }
        }
    }
}
