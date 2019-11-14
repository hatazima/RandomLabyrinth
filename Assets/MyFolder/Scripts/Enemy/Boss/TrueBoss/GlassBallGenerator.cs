using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlassBallGenerator : MonoBehaviour
{
    System.Random rand = new System.Random(Environment.TickCount);
    public GameObject player;
    public GameObject glassBall; //ガラスの玉を放つ
    float randomWait; //次に放つまでの時間をランダムにするための変数

    IEnumerator GlassShoot()
    {
        while(true)
        {
            randomWait = rand.Next(0, 10);
            randomWait = transform.localPosition.y * randomWait;
            randomWait = randomWait == 0 ?randomWait = 5 :randomWait;
            randomWait = randomWait < 0 ? randomWait = randomWait * -1 : randomWait;
            yield return new WaitForSeconds(randomWait);  //一時中断
            
            //弾のプレファブからインスタンスを作る
            GameObject obj = Instantiate(glassBall, transform.position, Quaternion.identity);
            //弾の発射方向を求める
            Vector3 vec = (player.transform.position - this.transform.position).normalized;
            //弾を発射する
            obj.GetComponent<Rigidbody>().velocity = vec * 3;
        }
    }

    void Start()
    {
        StartCoroutine("GlassShoot");
    }
}
