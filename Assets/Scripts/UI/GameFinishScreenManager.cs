using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameFinishScreenManager : MonoBehaviour
{
    [SerializeField] GameObject winPanel; // UI panel displayed when the player wins
    [SerializeField] GameObject losePanel; // UI panel displayed when the player loses

    [SerializeField] GameObject container; // Container for the scaling animation
    [SerializeField] Vector3 scaleAnimationVector; // Target scale for the animation
    [SerializeField] float animationTime; // Duration of the scaling animation

    [SerializeField] TMP_Text winResText; // Text element to display win results
    [SerializeField] TMP_Text loseResText; // Text element to display lose results

    [SerializeField] GameData gameData; // Reference to the GameData ScriptableObject for tracking game stats

    [SerializeField] MonoBehaviour[] scriptsToStop; // Array of scripts to disable upon game finish
    [SerializeField] GameObject[] objectsToStop; // Array of GameObjects to deactivate upon game finish

    public UnityEvent OnEndSession; // UnityEvent triggered at the end of a session

    private void Start()
    {
        // Subscribe to events for handling win and lose scenarios
        EventsManager.OnWinEvent += ShowWinUI;
        EventsManager.OnLoseEvent += ShowLoseUI;
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        EventsManager.OnWinEvent -= ShowWinUI;
        EventsManager.OnLoseEvent -= ShowLoseUI;
    }

    // Displays the win screen and performs necessary cleanup
    void ShowWinUI()
    {
        // Play a scaling animation on the container with a ping-pong effect
        LeanTween.scale(container, scaleAnimationVector, animationTime).setEaseInOutSine().setLoopPingPong(1);

        // Display the results on the win panel
        winResText.text = $"{gameData.NumberOfCorrectHits}/{gameData.MaxNumberOfCorrectHits}";
        winPanel.SetActive(true); // Activate the win panel
        losePanel.SetActive(false); // Deactivate the lose panel

        StopCertainObjects(); // Disable specified scripts and objects

        OnEndSession?.Invoke(); // Invoke the end session event for standalone versions
    }

    // Displays the lose screen and performs necessary cleanup
    void ShowLoseUI()
    {
        // Play a scaling animation on the container with a ping-pong effect
        LeanTween.scale(container, scaleAnimationVector, animationTime).setEaseInOutSine().setLoopPingPong(1);

        // Display the results on the lose panel
        loseResText.text = $"{gameData.NumberOfCorrectHits}/{gameData.MaxNumberOfCorrectHits}";
        winPanel.SetActive(false); // Deactivate the win panel
        losePanel.SetActive(true); // Activate the lose panel

        StopCertainObjects(); // Disable specified scripts and objects

        OnEndSession?.Invoke(); // Invoke the end session event for standalone versions
    }

    // Disables specific scripts and deactivates specific objects
    private void StopCertainObjects()
    {
        // Disable all scripts in the specified array
        for (int i = 0; i < scriptsToStop.Length; i++)
        {
            scriptsToStop[i].enabled = false;
        }  
        
        // Deactivate all objects in the specified array
        for (int i = 0; i < objectsToStop.Length; i++)
        {
            objectsToStop[i].SetActive(false);
        }
    }
}
