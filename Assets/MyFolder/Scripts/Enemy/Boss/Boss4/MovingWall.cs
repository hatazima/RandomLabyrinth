using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    void Update()
    {
        transform.Translate(-0.001f, 0, 0);
    }
}
