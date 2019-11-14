using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss2 : MonoBehaviour
{
    System.Random rand = new System.Random(Environment.TickCount);
    float commonRandX = 0, commonRandZ = 0; //様々なことに使う乱数
    public GameObject player;        //プレイヤーの位置を知るための変数
    int hp = 200;                    //ボスのHP
    public GameObject igaguri;       //攻撃パターン1 イガグリを降らせる
    public GameObject darkExplosion; //攻撃パターン2 悪の波動を発する
    public GameObject root;          //攻撃パターン3 根っこが大量に生えてくる
    public GameObject clearSceneGo;  //ボスの撃破時にクリアシーンに移動するための光が出てくる

    int attackPattern = 0;           //攻撃をランダムに決める乱数

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
                StartCoroutine("IgaguriFall");
                break;
            case 1:
                StartCoroutine("DarkExplosion");
                break;
            case 2:
                StartCoroutine("Root");
                break;
        }
    }

    IEnumerator IgaguriFall()
    {
        for(int i = 0; i < 12; i++)
        {
            commonRandX = rand.Next(-50, 50);
            commonRandZ = rand.Next(-50, 50);
            commonRandX *= 0.1f;
            commonRandZ *= 0.1f;
            Instantiate(igaguri, new Vector3(commonRandX, 4.7f, commonRandZ), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);  //一時中断
        }
        StartCoroutine("ActionSelection");
    }

    IEnumerator DarkExplosion()
    {
        darkExplosion.SetActive(true);
        yield return new WaitForSeconds(1f);  //一時中断
        darkExplosion.SetActive(false);
        StartCoroutine("ActionSelection");
    }

    IEnumerator Root()
    {
        for (int i = 0; i < 35; i++)
        {
            commonRandX = rand.Next(-80, 80);
            commonRandZ = rand.Next(-80, 80);
            commonRandX *= 0.1f;
            commonRandZ *= 0.1f;
            GameObject obj = Instantiate(root, new Vector3(commonRandX, -4, commonRandZ), Quaternion.identity);
            obj.transform.Rotate(-90, 0, 0);
            yield return new WaitForSeconds(0.2f);  //一時中断
        }
        StartCoroutine("ActionSelection");
    }

    void Start()
    {
        StartCoroutine("ActionSelection");
    }
    
    void Update()
    {
        transform.Rotate(0, -0.2f, 0);
    }
}
