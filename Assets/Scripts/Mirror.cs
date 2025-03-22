//The project is licensed under GPL-3.0, which requires all modifications and distributions to adhere to the same license.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    GameObject[] Mirrors;
    public int i = 0;

    public void PlaceMiror(GameObject mirror)
    { 
        if (i >= 2)
        {
            return;
        }
        i++;
        mirror.SetActive(true);
    }
}
