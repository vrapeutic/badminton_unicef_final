//The project is licensed under GPL-3.0, which requires all modifications and distributions to adhere to the same license.

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
