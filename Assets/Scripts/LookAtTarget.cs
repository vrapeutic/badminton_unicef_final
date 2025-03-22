//The project is licensed under GPL-3.0, which requires all modifications and distributions to adhere to the same license.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] Transform targetLookAt;
    void Update()
    {
        transform.LookAt(targetLookAt);        
    }
}
