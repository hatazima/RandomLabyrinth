using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSeed : MonoBehaviour
{ 
    public GameObject trueBoss; //ボス
    public GameObject blackout; //画面を暗くするためのオブジェクト
    public GameObject bossWord; //ボスの言葉のためのオブジェクト
    int hp = 5;                 //種のHP
    bool die = false;           //種のHPがなくなったことを確認するためのもの
    string word;                //ボスの言葉

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            GetComponent<ParticleSystem>().Play();
            if (hp <= 0 && die == false)
            {
                StartCoroutine("Die");
                die = true;
            }
        }
        else　if (other.gameObject.CompareTag("PlayerSword"))
        {
            hp -= 2;
            GetComponent<ParticleSystem>().Play();
            if (hp <= 0 && die == false)
            {
                StartCoroutine("Die");
                die = true;
            }
        }
    }
    
    IEnumerator Die() //種のHPがなくなったら画面が暗くなり、ボスが言葉を発した後に出現する
    {
        blackout.SetActive(true);
        AudioManager.Instance.PlayBGM("TrueBoss"); //BGM再生
        yield return new WaitForSeconds(3f); //一時中断
        bossWord.SetActive(true);
        StartCoroutine("BossWord");
        yield return new WaitForSeconds(21.6f); //一時中断
        trueBoss.SetActive(true);
        Destroy(blackout);
        Destroy(bossWord,2.4f);
        Destroy(gameObject);
    }
    
    IEnumerator BossWord()　//ボスの言葉
    {
        Text wordText = bossWord.GetComponent<Text>();

        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0: word = "我は神なり"; break;
                case 1: word = "我に挑みし愚かなる者よ"; break;
                case 2: word = "我が前に息絶えるがいい"; break;
                case 3: word = "永遠に！！"; break;
            }
            wordText.text = word;
            yield return new WaitForSeconds(6f);  //一時中断
        }
    }
}
