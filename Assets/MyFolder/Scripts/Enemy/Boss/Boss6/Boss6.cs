using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss6 : MonoBehaviour
{
    System.Random rand = new System.Random(Environment.TickCount);
    public static float commonRand = 0; //様々なことに使う乱数
    public GameObject player;           //プレイヤーの位置を知るための変数
    Vector3 startPos;                   //ボスの初期位置
    int hp = 70;                        //ボスのHP
    public GameObject paperMan;         //攻撃パターン1 式紙のランダム突進・攻撃パターン4
    public GameObject paperManCircle;   //攻撃パターン2 式紙のかごめかごめ
    public GameObject paperSword;       //攻撃パターン3 式紙の剣
    public GameObject homingBall;       //攻撃パターン4 追尾するボール
    public GameObject clearSceneGo;     //ボスの撃破時にクリアシーンに移動するための光が出てくる

    int attackPattern = 0;              //攻撃をランダムに決める乱数
    bool rotateSword = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
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
    }

    IEnumerator ActionSelection()
    {
        attackPattern = rand.Next(0, 4);
        yield return new WaitForSeconds(3f);  //一時中断
        switch (attackPattern)
        {
            case 0:
                StartCoroutine("PaperManRush");
                break;
            case 1:
                StartCoroutine("PaperManCircle");
                break;
            case 2:
                StartCoroutine("PaperSword");
                break;
            case 3:
                StartCoroutine("HomingBall");
                break;
        }
    }

    IEnumerator PaperManRush()
    {
        for (int i = 0; i < 30; i++)
        {
            commonRand = rand.Next(-180, 180);
            commonRand = commonRand / 10;
            GameObject obj0 = Instantiate(paperMan, new Vector3(commonRand, 1, 20), Quaternion.identity);
            obj0.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -10);
            Destroy(obj0, 3);
            yield return new WaitForSeconds(0.2f);  //一時中断
        }
        commonRand = rand.Next(0, 2);
        StartCoroutine("ActionSelection");
    }

    IEnumerator PaperManCircle()
    {
        commonRand = rand.Next(0, 2);
        GameObject obj1 = Instantiate(paperManCircle, new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z), Quaternion.identity);
        Destroy(obj1, 7);
        yield return new WaitForSeconds(5f);  //一時中断
        commonRand = rand.Next(0, 2);
        StartCoroutine("ActionSelection");
    }

    IEnumerator PaperSword()
    {
        paperSword.SetActive(true);
        yield return new WaitForSeconds(0.5f);  //一時中断
        rotateSword = true;
        yield return new WaitForSeconds(5f);  //一時中断
        paperSword.SetActive(false);
        rotateSword = false;
        transform.localEulerAngles = new Vector3(0, 45, 0);
        commonRand = rand.Next(0, 2);
        StartCoroutine("ActionSelection");
    }

    IEnumerator HomingBall()
    {
        for (int i = 0; i < 4; i++)
        {
            commonRand = rand.Next(-1, 2);
            //弾のプレファブからインスタンスを作る
            GameObject obj0 = Instantiate(homingBall, new Vector3(transform.position.x, 1.1f, transform.position.z - 2.5f), Quaternion.identity);
            //弾の発射方向を求める
            Vector3 vec = (player.transform.position*commonRand - obj0.transform.position).normalized;
            //弾を発射する
            obj0.GetComponent<Rigidbody>().velocity = vec * 10;
            //3秒後に弾を消去する
            Destroy(obj0, 3);
            yield return new WaitForSeconds(0.5f);  //一時中断
        }
        commonRand = rand.Next(0, 2);
        StartCoroutine("ActionSelection");
    }

    void Start()
    {
        StartCoroutine("ActionSelection");
    }
    
    void Update()
    {
        if(rotateSword)
        {
            transform.Rotate(0, 4, 0);
        }
        
        if(commonRand % 2 == 0 && rotateSword == false && transform.position.x <= 18)
        {
            transform.Translate(0.04f, 0, 0.04f);
        }
        else if(commonRand % 2 == 1 && rotateSword == false && transform.position.x >= -18)
        {
            transform.Translate(-0.04f, 0, -0.04f);
        }
    }
}
