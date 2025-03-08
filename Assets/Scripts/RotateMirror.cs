using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMirror : MonoBehaviour
{
    Mirror _mirror;
    
    
    public void RotateMiror(GameObject mirror)
    {
        //MechanicManager.hasMirror_0 = true;
        _mirror = GetComponent<Mirror>();
        _mirror.i--;
        mirror.SetActive(false);
        //Destroy(mirror);
        //root.transform.Rotate(root.transform.rotation.eulerAngles + new Vector3(0f,90f, 0f), Space.World);
    }
}
