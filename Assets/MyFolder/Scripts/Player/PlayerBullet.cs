using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") != true)
        {
            Destroy(gameObject,0.1f);
        }
    }
    void Start()
    {
        //プレイヤーの姿勢を取得
        Transform trans = GameObject.Find("ThirdPersonController").transform;
        //プレイヤーの位置と弾の位置を合わせる
        transform.position = new Vector3(trans.position.x, trans.position.y + 1, trans.position.z);
        //プレイヤーの向きと玉の向きを合わせる
        transform.forward = trans.forward;

        //1秒で削除
        Destroy(gameObject, 1);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
