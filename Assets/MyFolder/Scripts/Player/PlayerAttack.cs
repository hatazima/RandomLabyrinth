using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    //プレイヤーが壁を壊す動作に関する変数
    public GameObject blockBreak; //Zキーでブロックを壊すオブジェクト(透明)
    public GameObject breakText;  //壊せる残り回数を表示するText
    int breakCount;               //ブロックブレイクののこり回数をカウントする変数

    //プレイヤーの正面から玉を打つ攻撃に関する変数
    Vector3 mPos;                   //マウスのポジション
    public GameObject playerBullet; //弾丸の攻撃

    //プレイヤーの正面から剣を放つ攻撃に関する変数
    public GameObject playerSword; //剣の攻撃

    //プレイヤーの回復に関する変数
    public GameObject player;      //プレイヤー自身
    public GameObject portionText; //回復できる残り回数を表示するText
    int portionCount;              //回復できる残り回数をカウントする変数

    float time = 0;


    void Start()
    {
        breakCount = PlayerData.playerBreakCount;
        portionCount = PlayerData.playerPortionCount;
        // 初期テキストの表示
        Text text1 = breakText.GetComponent<Text>();
        text1.text = "×" + breakCount;
        Text text2 = portionText.GetComponent<Text>();
        text2.text = "×" + portionCount;

    }
    
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            switch (AbilityChange.count)
            {
                case 0: StartCoroutine("Break"); break; //36～52 壁を壊すコルーチン
                case 1: Instantiate(playerBullet); break; //弾丸のプレファブ
                case 2: Instantiate(playerSword); break; //剣のプレファブ
                case 3: Portion(); break; //ポーションのメソッド
            }
        }

        //ブレイクカウントを増やす
        if(time >= 60)
        {
            BreakCountPlus();
            time = 0;
        }
    }

    IEnumerator Break()//マウスを右クリックしたときに壁を壊す処理
    {
        if (breakCount >= 1)
        {
            AudioManager.Instance.PlaySE("BlockBreak");
            blockBreak.SetActive(true);
            breakCount--;
            PlayerData.playerBreakCount = breakCount;

            // テキストの表示を入れ替える
            Text text = breakText.GetComponent<Text>();
            text.text = "×" + breakCount;
        }
        yield return new WaitForSeconds(0.2f);  //一時中断
        blockBreak.SetActive(false);
    }

    public void BreakCountPlus()
    {
        breakCount++;
        // テキストの表示を入れ替える
        Text text = breakText.GetComponent<Text>();
        text.text = "×" + breakCount;
    }

    void Portion()//マウスを右クリックしたときにHPを回復する処理
    {
        if (portionCount >= 1)
        {
            //AudioManager.Instance.PlaySE("BlockBreak");
            player.GetComponent<PlayerHp>().HpPlus();

            portionCount--;
            PlayerData.playerPortionCount = portionCount;

            // テキストの表示を入れ替える
            Text text = portionText.GetComponent<Text>();
            text.text = "×" + portionCount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PortionPlus"))
        {
            portionCount++;
            PlayerData.playerPortionCount = portionCount;
            // テキストの表示を入れ替える
            Text text = portionText.GetComponent<Text>();
            text.text = "×" + portionCount;
            Destroy(other.gameObject);
        }
    }
}
