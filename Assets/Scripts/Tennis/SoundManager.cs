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

    private void Start()
    {
       // GameController.Instance.OnBallCollide += PlaySFX;
    }

    private void OnDisable()
    {
       // GameController.Instance.OnBallCollide -= PlaySFX;
    }

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
