using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    void Update()
    {
        transform.Translate(0, 0, 0.08f);

        if(transform.position.y > 0)
        {
            Destroy(gameObject);
        }
    }
}
