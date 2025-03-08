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
