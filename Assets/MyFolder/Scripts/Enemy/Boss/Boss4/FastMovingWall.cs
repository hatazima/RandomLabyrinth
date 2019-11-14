using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMovingWall : MonoBehaviour
{
    void Update()
    {
        transform.Translate(-0.028f, 0, 0);
    }
}
