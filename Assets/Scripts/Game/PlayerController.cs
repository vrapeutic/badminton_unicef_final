using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;

 

    public void ResetPlayer(float amount)
    {

        playableDirector.time = amount;
        playableDirector.initialTime = 0;
        transform.position = Vector3.zero;
        playableDirector.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int scene)
    { 
        SceneManager.LoadScene(scene);
    }
}
