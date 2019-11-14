using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss5 : MonoBehaviour
{
    System.Random rand = new System.Random(Environment.TickCount);
    public GameObject player;              //プレイヤーの位置を知るための変数
    public GameObject bossBody;            //ボスの体の全体
    int hp = 15;                           //ボスのHP
    public GameObject aquaSplash;       //攻撃パターン1 周囲に水の塊を飛ばす
    public GameObject raindrops;       //攻撃パターン2 全体にダメージの雨を降らせる
    public GameObject clearSceneGo;        //ボスの撃破時にクリアシーンに移動するための光が出てくる

    int attackPattern = 0;                 //攻撃をランダムに決める乱数
    float raindropsX = 0; //raindropsのx軸に使う乱数
    float raindropsZ = 0; //raindropsのz軸に使う乱数
    bool move = false;


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
                Destroy(gameObject);
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
                Destroy(gameObject);
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
                StartCoroutine("AquaSplash");
                break;
            case 1:
                StartCoroutine("Raindrops");
                break;
            case 2:
                StartCoroutine("Giant");
                break;
        }
    }

    IEnumerator AquaSplash()
    {
        aquaSplash.SetActive(true);
        yield return new WaitForSeconds(3f);  //一時中断
        aquaSplash.SetActive(false);
        StartCoroutine("ActionSelection");
    }

    IEnumerator Raindrops()
    {
        for(int i = 0; i < 400; i++)
        {
            raindropsX = rand.Next(-200, 200);
            raindropsZ = rand.Next(-200, 200);
            raindropsX *= 0.1f;
            raindropsZ *= 0.1f;
            GameObject obj = Instantiate(raindrops, new Vector3(raindropsX,10,raindropsZ), Quaternion.identity);
            Destroy(obj, 2);
            yield return new WaitForSeconds(0.0005f);  //一時中断
        }
        StartCoroutine("ActionSelection");
    }

    IEnumerator Giant()
    {
        Transform myTransform = this.transform;
        Vector3 localScale = myTransform.localScale;
        localScale.x = 6.0f; // ローカル座標を基準にした、x軸方向へ2倍のサイズ変更
        localScale.y = 6.0f; // ローカル座標を基準にした、y軸方向へ2倍のサイズ変更
        localScale.z = 6.0f; // ローカル座標を基準にした、z軸方向へ2倍のサイズ変更
        myTransform.localScale = localScale;
        yield return new WaitForSeconds(7f);  //一時中断
        localScale.x = 1.5f; // ローカル座標を基準にした、x軸方向へ2倍のサイズ変更
        localScale.y = 1.5f; // ローカル座標を基準にした、y軸方向へ2倍のサイズ変更
        localScale.z = 1.5f; // ローカル座標を基準にした、z軸方向へ2倍のサイズ変更
        myTransform.localScale = localScale;

        StartCoroutine("ActionSelection");
    }

    void Start()
    {
        StartCoroutine("ActionSelection");
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        while(true)
        {
            yield return new WaitForSeconds(5f);  //一時中断
            move = true;
            yield return new WaitForSeconds(5f);  //一時中断
            move = false;
        }
    }

    void Update()
    {
        if(move) transform.Translate(-0.05f, 0, 0);
        else transform.Translate(0.05f, 0, 0);
    }
}
