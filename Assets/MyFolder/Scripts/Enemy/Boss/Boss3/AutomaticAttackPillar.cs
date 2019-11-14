using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticAttackPillar : MonoBehaviour
{
    public GameObject player;      //プレイヤーの位置を知るための変数
    public GameObject flyingLight; //攻撃

    IEnumerator FlyingLight()
    {
        while(true)
        {
            //弾のプレファブからインスタンスを作る
            GameObject obj0 = Instantiate(flyingLight, transform.position, Quaternion.identity);
            //弾の発射方向を求める
            Vector3 vec = (player.transform.position - this.transform.position).normalized;
            //弾を発射する
            obj0.GetComponent<Rigidbody>().velocity = vec * 8;
            //5秒後に弾を消去する
            Destroy(obj0, 5);
            yield return new WaitForSeconds(10f);  //一時中断
        }
    }

    void Start()
    {
        StartCoroutine("FlyingLight");
    }
}
