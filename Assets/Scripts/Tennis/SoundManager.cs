using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            if (Instance != this)
                Destroy(gameObject);
        }
    }

    #endregion
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip ballClip;
    [SerializeField] AudioClip correctClip;
    [SerializeField] AudioClip wrongClip;

   

    void PlaySFX(string name)
    {
        if (name == "OpponentBox")
        {
            audioSource.PlayOneShot(correctClip);
        }
        else if (name == "PlayerBox")
        {
            audioSource.PlayOneShot(wrongClip);
        }
        else
        {
            audioSource.PlayOneShot(ballClip);
        }

        
    }
}
