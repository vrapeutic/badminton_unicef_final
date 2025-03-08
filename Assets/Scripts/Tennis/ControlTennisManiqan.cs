using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTennisManiqan : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void PlayDemoAnimation()
    {
        NormalSpeed();
        animator.SetTrigger("play");
    }

    public void BackToIdle()
    {
        NormalSpeed();
        animator.ResetTrigger("play");
        animator.SetTrigger("idle");
    }

    public void PauseAnimator()
    {
        Time.timeScale = 0.0f;
    }

    public void NormalSpeed()
    {
        Time.timeScale = 1.0f;
    }
}
