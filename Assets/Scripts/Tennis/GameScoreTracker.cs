using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreTracker : MonoBehaviour
{
    [SerializeField] GameData gameData; // Reference to the GameData ScriptableObject for tracking the score

    private void Start()
    {
        // Subscribe to the OnCorrectHit event when the script starts
        EventsManager.OnCorrectHit += IncreaseScore;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnCorrectHit event when the script is destroyed to prevent memory leaks
        EventsManager.OnCorrectHit -= IncreaseScore;
    }

    // Method to increase the player's score when a correct hit is registered
    void IncreaseScore()
    {
        gameData.IncreaseNumberOfCorrrectHits(); // Increment the score in the GameData ScriptableObject
    }
}