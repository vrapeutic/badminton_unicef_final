using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] GameData gameData; // Reference to the game data scriptable object

    float MaxTime; // Maximum allowed time for the game session
    bool canCount; // Flag to determine whether the timer is active

    float _timer; // Internal timer to track elapsed time

    public bool CanCount => canCount; // Public getter for the canCount flag

    private void Start()
    {
        // Subscribes to events when the script starts
        EventsManager.OnGameStarted += StartGameTimer; // Starts the timer when the game starts
        EventsManager.OnGamePause += SetCanCountFalse; // Pauses the timer when the game is paused
        EventsManager.OnEndDistraction += SetCanCountTrue; // Resumes the timer after a distraction ends
    }

    private void OnDestroy()
    {
        // Unsubscribes from events when the script is destroyed
        EventsManager.OnGameStarted -= StartGameTimer;
        EventsManager.OnGamePause -= SetCanCountFalse;
        EventsManager.OnEndDistraction -= SetCanCountTrue;
    }

    private void Update()
    {
        if (canCount) // Check if the timer is allowed to count
        {
            _timer += Time.deltaTime; // Increment timer with the time passed since the last frame
            if (_timer > MaxTime) // Check if the timer has exceeded the maximum allowed time
            {
                canCount = false; // Stop the timer
                _timer = 0; // Reset the timer
                EventsManager.OnGameTimerFinish?.Invoke(gameData.NumberOfCorrectHits); // Trigger the timer finish event
                EventsManager.OnWriteCSV?.Invoke(); // Trigger the event to write data to a CSV file
            }
        }
    }

    // Method to start the game timer
    [ContextMenu("Start")]
    public void StartGameTimer()
    {
        if (canCount) return; // Prevents restarting the timer if it's already running

        MaxTime = gameData.MaxTime; // Set the maximum allowed time from the game data
        canCount = true; // Enable the timer
    }

    // Method to allow the timer to count
    private void SetCanCountTrue()
    {
        canCount = true;
    }

    // Method to stop the timer from counting
    private void SetCanCountFalse()
    {
        // Currently commented out; this would stop the timer if uncommented
        // canCount = false;
    }
}
