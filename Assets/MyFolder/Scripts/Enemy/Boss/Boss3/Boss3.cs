using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss3 : MonoBehaviour
{
    System.Random rand = new System.Random(Environment.TickCount);
    public GameObject player;              //プレイヤーの位置を知るための変数
    public GameObject bossBody;            //ボスの体の全体
    int hp = 40;                           //ボスのHP
    public GameObject rotatingBlade;       //攻撃パターン1 回転する刃
    public GameObject rotatingSingleBlade; //攻撃パターン2 広範囲に回転する片刃
    public GameObject flyingLight;         //攻撃パターン3 飛んでくる光
    public GameObject clearSceneGo;        //ボスの撃破時にクリアシーンに移動するための光が出てくる

    int attackPattern = 0;                 //攻撃をランダムに決める乱数

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            GetComponent<ParticleSystem>().Play();
            if (hp == 0)
            {
                //BGMフェードアウト
                AudioManager.Instance.FadeOutBGM();
                //BGM再生
                AudioManager.Instance.PlayBGM("Clear");
                clearSceneGo.SetActive(true);
                Destroy(bossBody);
            }
        }
        if (other.gameObject.CompareTag("PlayerSword"))
        {
            hp -= 2;
            GetComponent<ParticleSystem>().Play();
            if (hp <= 0)
            {
                //BGMフェードアウト
                AudioManager.Instance.FadeOutBGM();
                //BGM再生
                AudioManager.Instance.PlayBGM("Clear");
                clearSceneGo.SetActive(true);
                Destroy(bossBody);
            }
        }
    }

    IEnumerator ActionSelection()
    {
        attackPattern = rand.Next(0, 3);
        yield return new WaitForSeconds(3f);  //一時中断
        switch (attackPattern)
        {
            case 0:
                StartCoroutine("RotatingBlade");
                break;
            case 1:
                StartCoroutine("RotatingSingleBlade");
                break;
            case 2:
                StartCoroutine("FlyingLight");
                break;
        }
    }

    IEnumerator RotatingBlade()
    {
        rotatingBlade.SetActive(true);
        yield return new WaitForSeconds(7f);  //一時中断
        rotatingBlade.SetActive(false);
        StartCoroutine("ActionSelection");
    }

    IEnumerator RotatingSingleBlade()
    {
        rotatingSingleBlade.SetActive(true);
        yield return new WaitForSeconds(7f);  //一時中断
        rotatingSingleBlade.SetActive(false);
        StartCoroutine("ActionSelection");
    }

    IEnumerator FlyingLight()
    {
        for (int i = 0; i < 10; i++)
        {
            //弾のプレファブからインスタンスを作る
            GameObject obj0 = Instantiate(flyingLight, transform.position, Quaternion.identity);
            //弾の発射方向を求める
            Vector3 vec = (player.transform.position - this.transform.position).normalized;
            //弾を発射する
            obj0.GetComponent<Rigidbody>().velocity = vec * 4;
            //2秒後に弾を消去する
            Destroy(obj0, 2);
            yield return new WaitForSeconds(0.4f);  //一時中断
        }
        StartCoroutine("ActionSelection");
    }

    void Start()
    {
        StartCoroutine("ActionSelection");
    }
    
    void Update()
    {
        
    }
}
