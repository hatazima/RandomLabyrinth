using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerSword : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlaySE("PlayerSword");
        //プレイヤーの姿勢を取得
        Transform trans = FindObjectOfType<ThirdPersonCharacter>().gameObject.transform;
        //プレイヤーの位置と剣の位置を合わせる
        transform.position = new Vector3(trans.position.x, trans.position.y + 0.35f, trans.position.z);
        //プレイヤーの向きと剣の向きを合わせる
        transform.forward = trans.forward;
        //向きの調整
        transform.Rotate(0, 270, 0);

        //0.3秒で削除
        Destroy(gameObject, 0.35f);
    }
    
    void Update()
    {
        transform.Rotate(0, 8, 0);
    }
}
