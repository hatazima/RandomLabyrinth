using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Slider hpBar;
    public GameObject maxHpObject;
    public GameObject hpObject;
    float maxHp = 100;
    float hp = 100;
    float damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyAttack"))
        {
            hpBar.value -= damage * 10;
            // テキストの表示を入れ替える
            hp -= 10;
            Text text2 = hpObject.GetComponent<Text>();
            text2.text = "" + hp;
        }
        if(other.gameObject.CompareTag("HpPlus"))
        {
            hpBar.value = 1;
            maxHp = 200;
            hp = 200;
            hpBar = hpBar.GetComponent<Slider>();
            damage = hpBar.value / maxHp;

            // テキストの表示を入れ替える
            Text text1 = maxHpObject.GetComponent<Text>();
            text1.text = "" + maxHp;
            // テキストの表示を入れ替える
            Text text2 = hpObject.GetComponent<Text>();
            text2.text = "" + hp;

            Destroy(other.gameObject);
        }

    }

    void Start()
    {
        hpBar = hpBar.GetComponent<Slider>();
        damage = hpBar.value / maxHp;

        // 初期テキストの表示
        Text text1 = maxHpObject.GetComponent<Text>();
        text1.text = "" + maxHp;
        // 初期テキストの表示
        Text text2 = hpObject.GetComponent<Text>();
        text2.text = "" + hp;

    }
    
    public void HpPlus()
    {
        hpBar.value = 1;
        hp = maxHp;
        Text text = hpObject.GetComponent<Text>();
        text.text = "" + hp;
    }
}
