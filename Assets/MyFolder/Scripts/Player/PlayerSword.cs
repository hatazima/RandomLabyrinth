using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    void Start()
    {
        //プレイヤーの姿勢を取得
        Transform trans = GameObject.Find("ThirdPersonController").transform;
        //プレイヤーの位置と弾の位置を合わせる
        transform.position = new Vector3(trans.position.x, trans.position.y + 0.35f, trans.position.z);
        //プレイヤーの向きと玉の向きを合わせる
        transform.forward = trans.forward;
        //向きの調整
        transform.Rotate(0, 270, 0);

        //1秒で削除
        Destroy(gameObject, 0.3f);
    }
    
    void Update()
    {
        transform.Rotate(0, 10, 0);
    }
}
