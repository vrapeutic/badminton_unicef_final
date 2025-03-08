using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    //[SerializeField] MechanicManager mechanicManager;

    GameObject[] Mirrors;
    public int i = 0;

    public void PlaceMiror(GameObject mirror)
    {

        /*Mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        foreach (GameObject go in Mirrors)
        {
            if (go.activeSelf == true)
            {
                i++;
            }
        }*/
        
        if (i >= 2)
        {
            return;
        }
        i++;
        mirror.SetActive(true);
    }
}
