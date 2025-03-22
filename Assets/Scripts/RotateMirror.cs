//The project is licensed under GPL-3.0, which requires all modifications and distributions to adhere to the same license.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMirror : MonoBehaviour
{
    Mirror _mirror;
    
    
    public void RotateMiror(GameObject mirror)
    {
        _mirror = GetComponent<Mirror>();
        _mirror.i--;
        mirror.SetActive(false);
    }
}
