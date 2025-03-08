using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicManager : MonoBehaviour
{
    public static bool hasMirror_0;
    GameObject[] Mirrors;
    public int i = 0;


    void Start()
    {
        
    }

    
    void Update()
    {
        Mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        foreach(GameObject go in Mirrors)
        {
            if(go.activeSelf == true)
            {
                i++;
            }
        }


    }
}
