using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    System.Random rand = new System.Random(Environment.TickCount);
    public GameObject player;      //プレイヤーの位置を知るための変数
    Animator animator;             //ボスのアニメーション
    int hp = 100;                   //ボスのHP
    public GameObject beam;        //攻撃パターン１ 口から玉を吐く
    public GameObject needle;      //攻撃パターン２ とげを召喚する
    public GameObject sword;       //攻撃パターン３ 剣？を召喚する
    public GameObject rollingPole; //攻撃パターン４ 転がる棒を召喚する
    public GameObject clearSceneGo;     //ボスの撃破時にクリアシーンに移動するための光が出てくる

    int attackPattern = 0;         //攻撃をランダムに決める乱数

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
        attackPattern = rand.Next(0, 4);
        yield return new WaitForSeconds(3.5f);  //一時中断
        this.animator.SetInteger("Action", 0);
        yield return new WaitForSeconds(1f);  //一時中断
        switch(attackPattern)
        {
            case 0:
                StartCoroutine("Beam");
                break;
            case 1:
                StartCoroutine("Needle");
                break;
            case 2:
                StartCoroutine("Sword");
                break;
            case 3:
                StartCoroutine("RollingPole");
                break;
        }
    }

    IEnumerator Beam()
    {
        this.animator.SetInteger("Action", 1);
        for(int i = 0; i < 12; i++)
        {
            //弾のプレファブからインスタンスを作る
            GameObject obj0 = Instantiate(beam, new Vector3(transform.position.x, transform.position.y, transform.position.z - 3), Quaternion.identity);
            //弾の発射方向を求める
            Vector3 vec = (player.transform.position - this.transform.position).normalized;
            //弾を発射する
            obj0.GetComponent<Rigidbody>().velocity = vec * 10;
            //2秒後に弾を消去する
            Destroy(obj0, 2);
            yield return new WaitForSeconds(0.5f);  //一時中断
        }
        StartCoroutine("ActionSelection");
    }

    IEnumerator Needle()
    {
        this.animator.SetInteger("Action", 2);
        for (int i = 16; i >= -16; i-=2)
        {
            for (int j = -16; j <= 16; j+=4)
            {
                GameObject obj1 = Instantiate(needle, new Vector3(j, 0, i), Quaternion.identity);
                Destroy(obj1, 2);
            }
            yield return new WaitForSeconds(0.2f);  //一時中断
        }
        for (int i = 16; i >= -16; i-=2)
        {
            for (int j = -18; j <= 18; j+=4)
            {
                GameObject obj1 = Instantiate(needle, new Vector3(j, 0, i), Quaternion.identity);
                Destroy(obj1, 2);
            }
            yield return new WaitForSeconds(0.2f);  //一時中断
        }
        StartCoroutine("ActionSelection");
        this.animator.SetInteger("Action", 3);
    }

    IEnumerator Sword()
    {
        this.animator.SetInteger("Action", 2);
        GameObject obj2 = Instantiate(sword);
        Destroy(obj2, 12);
        yield return new WaitForSeconds(12f);  //一時中断
        StartCoroutine("ActionSelection");
        this.animator.SetInteger("Action", 3);
    }

    IEnumerator RollingPole()
    {
        this.animator.SetInteger("Action", 2);
        GameObject obj3 = Instantiate(rollingPole);
        Destroy(obj3, 25);
        yield return new WaitForSeconds(25f);  //一時中断
        StartCoroutine("ActionSelection");
        this.animator.SetInteger("Action", 3);
    }

    void Start()
    {
        AudioManager.Instance.PlayBGM("Boss");
        this.animator = GetComponent<Animator>();
        this.animator.SetInteger("Action", 0);
        StartCoroutine("ActionSelection");
    }
}
