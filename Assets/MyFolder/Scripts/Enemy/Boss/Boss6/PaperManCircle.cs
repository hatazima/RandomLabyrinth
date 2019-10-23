using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;

public class PaperManCircle : MonoBehaviour
{
    public GameObject[] parerMan;
    bool attackMethod = false; // 攻撃の種類

    void Start()
    {
        GameObject player = FindObjectOfType<ThirdPersonCharacter>().gameObject;
        if (Boss6.commonRand == 0)
        {
            attackMethod = true;
        }
        for(int i = 0; i < parerMan.Length; i++)
        {
            if (attackMethod)
            {
                //式紙の進行方向を求める
                Vector3 vec = (player.transform.position - parerMan[i].transform.position).normalized;
                //式紙を発射する
                parerMan[i].GetComponent<Rigidbody>().velocity = vec * 3f;
            }
        }
    }
    
    void Update()
    {
        transform.Rotate(0, 1, 0);

        if (attackMethod == false)
        {
            for (int i = 0; i < parerMan.Length; i++)
            {
                parerMan[i].transform.Translate(0, 0, -0.02f);
            }
        }
    }
}
