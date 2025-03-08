using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public void Red()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void Blue()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    public void Yellow()
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
}
