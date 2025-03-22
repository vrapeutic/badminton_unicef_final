//The project is licensed under GPL-3.0, which requires all modifications and distributions to adhere to the same license.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ActivateShoot()
    {
        Debug.Log("Shoot");
        anim.SetBool("Shoot",true);
    }
}
