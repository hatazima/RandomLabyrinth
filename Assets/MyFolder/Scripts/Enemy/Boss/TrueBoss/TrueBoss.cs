using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrueBoss : MonoBehaviour
{
    System.Random rand = new System.Random(Environment.TickCount);
    public GameObject[] bossBody;      //ボスの体・ダメージに応じて体が削れる
    public GameObject[] brokenParts;   //ボスの壊れた体のパーツ
    public GameObject player;          //プレイヤーの位置を知るための変数
    public GameObject particleObj;     //パーティクルを発生させるオブジェクト
    int hp = 24;                       //ボスのHP
    public GameObject sword;           //攻撃パターン1 剣？を召喚する
    public GameObject igaguri;         //攻撃パターン2 イガグリを降らせる
    public GameObject raindrops;       //攻撃パターン3 全体にダメージの雨を降らせる
    public GameObject paperManCircle;  //攻撃パターン4 式紙のかごめかごめ
    public GameObject clearSceneGo;    //ボスの撃破時にクリアシーンに移動するための光が出てくる

    int attackPattern = 0;                //攻撃をランダムに決める乱数
    int brokenCondition = 21;          //ボスの体の壊れ具合
    int partsSelection1 = 0, partsSelection2 = 1; //ボスの壊す体の選択
    int brokenPartsCount = 0;
    float raindropsX = 0, raindropsZ = 0; //x,z軸に使う乱数

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            if (hp == 0)
            {
                //BGMフェードアウト
                AudioManager.Instance.FadeOutBGM();
                //BGM再生
                AudioManager.Instance.PlayBGM("Clear");
                clearSceneGo.SetActive(true);
                Destroy(bossBody[14]);
            }
        }
        if (other.gameObject.CompareTag("PlayerSword"))
        {
            hp -= 2;
            if (hp <= 0)
            {
                //BGMフェードアウト
                AudioManager.Instance.FadeOutBGM();
                //BGM再生
                AudioManager.Instance.PlayBGM("Clear");
                clearSceneGo.SetActive(true);
                Destroy(bossBody[14]);
            }
        }
        if (other.gameObject.CompareTag("EnemyAttack") && hp > 3)
        {
            hp--;
            particleObj.GetComponent<ParticleSystem>().Play();
            //ボスの体をダメージに応じて壊す
            if (hp <= brokenCondition)
            {
                if(brokenPartsCount < 6)
                {
                    brokenParts[brokenPartsCount].SetActive(true);
                    Destroy(brokenParts[brokenPartsCount],3);
                    brokenPartsCount++;
                }
                Destroy(bossBody[partsSelection1]);
                Destroy(bossBody[partsSelection2]);
                partsSelection1 += 2;
                partsSelection2 += 2;
                brokenCondition -= 3;
            }
            if(hp == 3)
            {
                StartCoroutine("BossTackle");
            }
        }
    }

    IEnumerator ActionSelection()
    {
        attackPattern = rand.Next(0, 4);
        yield return new WaitForSeconds(3f);  //一時中断
        switch (attackPattern)
        {
            case 0:
                StartCoroutine("Sword");
                break;
            case 1:
                StartCoroutine("IgaguriFall");
                break;
            case 2:
                StartCoroutine("Raindrops");
                break;
            case 3:
                StartCoroutine("PaperManCircle");
                break;
        }
    }

    IEnumerator Sword()
    {
        GameObject obj = Instantiate(sword);
        Destroy(obj, 12);
        yield return new WaitForSeconds(12f);  //一時中断
        StartCoroutine("ActionSelection");
    }

    IEnumerator IgaguriFall()
    {
        for (int i = 0; i < 25; i++)
        {
            raindropsX = rand.Next(-120, 120);
            raindropsZ = rand.Next(-120, 120);
            raindropsX *= 0.1f;
            raindropsZ *= 0.1f;
            Instantiate(igaguri, new Vector3(raindropsX, 8f, raindropsZ), Quaternion.identity);
            yield return new WaitForSeconds(0.25f);  //一時中断
        }
        StartCoroutine("ActionSelection");
    }
    
    IEnumerator Raindrops()
    {
        
        for (int i = 0; i < 300; i++)
        {
            raindropsX = rand.Next(-240, 240);
            raindropsZ = rand.Next(-120, 120);
            raindropsX *= 0.1f;
            raindropsZ *= 0.1f;
            GameObject obj = Instantiate(raindrops, new Vector3(raindropsX, 10, raindropsZ), Quaternion.identity);
            Destroy(obj, 2);
            yield return new WaitForSeconds(0.0005f);  //一時中断
        }
        StartCoroutine("ActionSelection");
    }

    IEnumerator PaperManCircle()
    {
        GameObject obj = Instantiate(paperManCircle, new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z), Quaternion.identity);
        Destroy(obj, 7);
        yield return new WaitForSeconds(5f);  //一時中断
        StartCoroutine("ActionSelection");
    }

    IEnumerator BossTackle()
    {
        yield return new WaitForSeconds(2f);  //一時中断
        while (true)
        {
            //弾の発射方向を求める
            Vector3 vec = (player.transform.position - this.transform.position).normalized;
            //弾を発射する
            GetComponent<Rigidbody>().velocity = vec * 8;
            yield return new WaitForSeconds(5f);  //一時中断
        }
    }

    void Start()
    {
        StartCoroutine("ActionSelection");
    }
    
    void Update()
    {
        
    }
}
