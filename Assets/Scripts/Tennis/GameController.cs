using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    #region Singleton

    public static GameController Instance; // Singleton instance for global access

    private void Awake()
    {
        // Initialize the Singleton instance
        if (Instance == null)
            Instance = this;
        else
        {
            if (Instance != this)
                Destroy(gameObject); // Destroy duplicate instances
        }
    }

    #endregion

    [SerializeField] InputActionReference simulatorAction; // Input action for simulator
    [SerializeField] InputActionReference controllerAction; // Input action for controller
    [SerializeField] BallShooter ballShooter; // Reference to the BallShooter script
    [SerializeField] GameData gameData; // Reference to the GameData ScriptableObject

    [SerializeField] OpponentAIDrriver opponentAIDrriver; // Reference to the primary AI driver
    [SerializeField] TimeManager timeManager; // Reference to the TimeManager

    [SerializeField] OpponentAIDrriver[] AIs; // Array of opponent AI drivers

    // Events
    public Action OnBallCollide; // Event triggered when the ball collides
    void Start()
    {
        //simulatorAction.action.performed += ctx => MyInputActionHandler();
        //controllerAction.action.performed += ctx => MyInputActionHandler();
        
        // Subscribe to events
        EventsManager.OnGameTimerFinish += CheckForStates;
        EventsManager.OnGameResume += ForceAIToShootBall;
        OnBallCollide += ShootBallAgain;

        // Start the game after a delay of 4 seconds
        Invoke("StartGame", 4f);
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        EventsManager.OnGameTimerFinish -= CheckForStates;
        EventsManager.OnGameResume -= ForceAIToShootBall;
        OnBallCollide -= ShootBallAgain; // Fixed from "OnBallCollide += ShootBallAgain"
    }

    void Update()
    {
        // Check if the simulator input is pressed
        if (simulatorAction.action.IsPressed())
        {
            MyInputActionHandler();
        }

        // Check if the controller input is pressed
        if (controllerAction.action.IsPressed())
        {
            MyInputActionHandler();
        }
    }

    // Increases the number of hits in the game data
    public void IncreaseNumberOfHits()
    {
        gameData.IncreaseNumberOfCorrrectHits();
    }

    // Handles the input action for triggering ball strikes
    void MyInputActionHandler()
    {
        opponentAIDrriver.CustomStrikeBall(); // Perform a custom strike using the AI driver
        EventsManager.OnGameStarted?.Invoke(); // Trigger the game start event
    }

    // Starts the game and initializes AI behavior
    [ContextMenu("Start")]
    public void StartGame() // UI
    {
        EnableAI(); // Enable all AI drivers
        opponentAIDrriver.CustomStrikeBall(); // Perform an initial ball strike
        EventsManager.OnGameStarted?.Invoke(); // Trigger the game start event
    }

    // Forces the AI to shoot the ball if the timer is not counting
    private void ForceAIToShootBall()
    {
        if (timeManager.CanCount) return; // Skip if the timer is active
        opponentAIDrriver.CustomStrikeBall(); // Perform a custom strike using the AI driver
    }

    // Shoots the ball again after a delay
    private void ShootBallAgain()
    {
        LeanTween.delayedCall(1.5f, () => { opponentAIDrriver.CustomStrikeBall(); });
    }

    // Enables all AI drivers and their colliders
    void EnableAI()
    {
        for (int i = 0; i < AIs.Length; i++)
        {
            AIs[i].enabled = true;
            AIs[i].GetComponent<Collider>().enabled = true;
        }
    }

    // Disables all AI drivers and their colliders
    void DisableAI()
    {
        for (int i = 0; i < AIs.Length; i++)
        {
            AIs[i].enabled = false;
            AIs[i].GetComponent<Collider>().enabled = false;
        }
    }

    // Checks the game state based on the number of hits and triggers win/lose events
    void CheckForStates(int currentNumberOfHits)
    {
        DisableAI(); // Disable all AI drivers

        if (currentNumberOfHits >= gameData.MaxNumberOfCorrectHits)
        {
            EventsManager.OnWinEvent?.Invoke(); // Trigger the win event if the target is met
        }
        else
        {
            EventsManager.OnLoseEvent?.Invoke(); // Trigger the lose event otherwise
        }
    }
}