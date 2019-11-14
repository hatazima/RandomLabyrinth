using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector2Int roomPosition { get; set; }
    WorldCreate wc;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("BlockBreak"))
        {
            wc.SetMapType(roomPosition, MapType.empty);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        wc = FindObjectOfType<WorldCreate>();
    }
}
