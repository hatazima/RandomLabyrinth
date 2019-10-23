using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public GameObject floor;
    public GameObject[] wall;
    public Material[] floorMaterial;
    public Material[] wallMaterial;

    void Start()
    {
        int type = WorldCreate.materialType;
        floor.GetComponent<Renderer>().material = floorMaterial[type];
        
        foreach (GameObject obj in wall)
        {
            obj.GetComponent<Renderer>().material = wallMaterial[type];
        }
    }
    
    void Update()
    {
        
    }
}
