using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityChange : MonoBehaviour
{
    public Image[] subIcon; //サブアイコンのオブジェクト
    public Material[] material;  //アビリティアイコンのマテリアル
    public static int count = 0; //アビリティを保存する変数
    Vector2 mouseWheel;          //マウスのホイールを保存する変数

    void Start()
    {
        for(int i = 0; i <= 3; i++)
        {
            subIcon[i].GetComponent<Image>().material.color = new Color(0.3f, 0.3f, 0.3f);
        }
        subIcon[count].GetComponent<Image>().material.color = new Color(1f, 1f, 1f);
    }
    
    void Update()
    {
        this.GetComponent<Image>().material = material[count];
        mouseWheel = Input.mouseScrollDelta;

        if (mouseWheel.y == 1 && count < 3)
        {
            subIcon[count].GetComponent<Image>().material.color = new Color(0.3f, 0.3f, 0.3f);
            count++;
            subIcon[count].GetComponent<Image>().material.color = new Color(1f, 1f, 1f);
        }
        else if (mouseWheel.y == 1 && count == 3)
        {
            subIcon[count].GetComponent<Image>().material.color = new Color(0.3f, 0.3f, 0.3f);
            count = 0;
            subIcon[count].GetComponent<Image>().material.color = new Color(1f, 1f, 1f);
        }

        if (mouseWheel.y == -1 && count > 0)
        {
            subIcon[count].GetComponent<Image>().material.color = new Color(0.3f, 0.3f, 0.3f);
            count--;
            subIcon[count].GetComponent<Image>().material.color = new Color(1f, 1f, 1f);
        }
        else if (mouseWheel.y == -1 && count == 0)
        {
            subIcon[count].GetComponent<Image>().material.color = new Color(0.3f, 0.3f, 0.3f);
            count = 3;
            subIcon[count].GetComponent<Image>().material.color = new Color(1f, 1f, 1f);
        }
    }
}
