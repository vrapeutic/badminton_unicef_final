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
