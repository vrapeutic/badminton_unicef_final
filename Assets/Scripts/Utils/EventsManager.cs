using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class serves as a centralized manager for game events using static Actions
public class EventsManager
{
    // Triggered when the game timer finishes, passing the final score or number of correct hits
    public static Action<int> OnGameTimerFinish;

    // Triggered when the game starts
    public static Action OnGameStarted;

    // Triggered when the player wins the game
    public static Action OnWinEvent;

    // Triggered when the player loses the game
    public static Action OnLoseEvent;

    // Triggered when the player makes a correct hit
    public static Action OnCorrectHit;

    // Triggered when a distraction event begins
    public static Action OnPlayDistraction;

    // Triggered when a distraction event ends
    public static Action OnEndDistraction;

    // Triggered when a button interaction occurs, passing the specific button instance
    public static Action<OnButtonInteractionUI> OnButtonClicked;

    // Triggered to pause the game
    public static Action OnGamePause;

    // Triggered to resume the game
    public static Action OnGameResume;

    // Sends a subtitle text to the UI, passing a string parameter for the subtitle content
    public static Action<string> OnSendSubtitleUI;

    // Triggered to signal that data should be written to a CSV file
    public static Action OnWriteCSV;
}