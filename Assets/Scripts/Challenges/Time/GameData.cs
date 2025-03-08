using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a ScriptableObject for storing game-related data
[CreateAssetMenu(fileName = "Time", menuName = "Time/TimeData")]
public class GameData : ScriptableObject
{
    [SerializeField] float maxTime; // Maximum time allowed for a game session
    [SerializeField] int maxNumberOfCorrectHits; // Maximum number of correct hits achievable
    [SerializeField] int numberOfCorrectHits; // Current number of correct hits achieved

    // Public property to get the maximum time
    public float MaxTime => maxTime;

    // Public property to get or set the maximum number of correct hits
    public int MaxNumberOfCorrectHits
    {
        get { return maxNumberOfCorrectHits; }
        set { maxNumberOfCorrectHits = value; }
    }

    // Public property to get or set the current number of correct hits
    public int NumberOfCorrectHits
    {
        get { return numberOfCorrectHits; }
        set { numberOfCorrectHits = value; }
    }

    // Method to update the maximum time
    public void SetMaxTime(float _value)
    {
        maxTime = _value;
    }

    // Method to increment the number of correct hits
    public void IncreaseNumberOfCorrrectHits()
    {
        numberOfCorrectHits++; // Increment the count of correct hits
        Debug.Log(numberOfCorrectHits); // Log the updated number of correct hits
    }
}