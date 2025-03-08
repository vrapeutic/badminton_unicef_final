using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisGameScore : MonoBehaviour
{

    public int OpponentScore = 0;
    public int playerScore = 0;

    void Start()
    {
        ResetScore();
       // GameController.Instance.OnBallCollide += UpdateScore;
    }
    private void OnDisable()
    {
       // GameController.Instance.OnBallCollide -= UpdateScore;
    }

    void UpdateScore(string name)
    {
        if(name == "OpponentBox")
        {
            playerScore++;
        }
        else if (name == "PlayerBox")
        {
            OpponentScore++;
        }
    }

    void ResetScore()
    {
        OpponentScore = 0;
        playerScore = 0;
    }
}
