using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss4 : MonoBehaviour
{
    System.Random rand = new System.Random(Environment.TickCount);
    public GameObject player;              //プレイヤーの位置を知るための変数
    int hp = 50;                           //ボスのHP
    public GameObject sandBall;       //攻撃パターン1 攻撃を受けたときに砂の玉で反撃をする。
    public GameObject clearSceneGo;        //ボスの撃破時にクリアシーンに移動するための光が出てくる

    bool reverseMove = true;

    private void OnTriggerEnter(Collider other)
    {
        if (reverseMove) reverseMove = false;
        else reverseMove = true;

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            GetComponent<ParticleSystem>().Play();
            SandBall();
            if (hp == 0)
            {
                //BGMフェードアウト
                AudioManager.Instance.FadeOutBGM();
                //BGM再生
                AudioManager.Instance.PlayBGM("Clear");
                clearSceneGo.SetActive(true);
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("PlayerSword"))
        {
            hp -= 2;
            GetComponent<ParticleSystem>().Play();
            SandBall();
            if (hp <= 0)
            {
                //BGMフェードアウト
                AudioManager.Instance.FadeOutBGM();
                //BGM再生
                AudioManager.Instance.PlayBGM("Clear");
                clearSceneGo.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if(reverseMove) transform.Translate(0.1f, 0, 0);
        else transform.Translate(-0.1f, 0, 0);

    }

    void SandBall()
    {
        //弾のプレファブからインスタンスを作る
        GameObject obj0 = Instantiate(sandBall, transform.position, Quaternion.identity);
        //弾の発射方向を求める
        Vector3 vec = (player.transform.position - this.transform.position).normalized;
        //弾を発射する
        obj0.GetComponent<Rigidbody>().velocity = vec * 10;
        //2秒後に弾を消去する
        Destroy(obj0, 2);
    }
}
