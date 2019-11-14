using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;

public class GlassBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerSword"))
        {
            GameObject boss;
            //ボスの位置を取得
            boss = FindObjectOfType<TrueBoss>().gameObject;
            //弾の発射方向を求める
            Vector3 vec = (boss.transform.position - this.transform.position).normalized;
            //弾を発射する
            GetComponent<Rigidbody>().velocity = vec * 8;
        }
        if (other.gameObject.CompareTag("TrueBoss"))
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        //GameObject player;
        ////プレイヤーの位置を取得
        //player = FindObjectOfType<ThirdPersonCharacter>().gameObject;
        ////弾の発射方向を求める
        //Vector3 vec = (player.transform.position - this.transform.position).normalized;
        ////弾を発射する
        //GetComponent<Rigidbody>().velocity = vec * 3;
    }
    
    void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
