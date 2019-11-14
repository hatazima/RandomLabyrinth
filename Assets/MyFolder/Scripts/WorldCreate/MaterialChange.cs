using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public GameObject floor;
    public GameObject[] wall;
    Material[] floorMaterial;
    Material[] wallMaterial;

    void Start()
    {
        floorMaterial = Resources.LoadAll<Material>("Floor");
        wallMaterial = Resources.LoadAll<Material>("Wall");
        int type = WorldCreate.type;
        floor.GetComponent<Renderer>().material = floorMaterial[type];
        
        foreach (GameObject obj in wall)
        {
            obj.GetComponent<Renderer>().material = wallMaterial[type];
        }
    }
}
