using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumeration to define distraction levels
public enum DistractionLevel
{
    None, // No distractions
    L1,   // Level 1 distraction
    L2,   // Level 2 distraction
    L3    // Level 3 distraction
}

public class DistractionManager : MonoBehaviour
{
    #region Singleton

    public static DistractionManager Singleton; // Singleton instance for global access

    private void Awake()
    {
        if (Singleton == null)
            Singleton = this; // Assign this instance as the Singleton
        else
        {
            if (Singleton != this)
                Destroy(gameObject); // Destroy duplicate instances
        }

        // Subscribe to distraction start and end events
        OnDistarctionStart += DebugDistractionStart;
        OnDistarctionEnd += DebugDistractionEnd;
        EventsManager.OnEndDistraction += ResetTimer;
    }

    #endregion

    [SerializeField] float timeToPlayNextDistract; // Time interval between distractions
    [SerializeField] VFXPlayer birdDist; // Visual effects for bird distraction
    [SerializeField] RandomPickerMoving movingDist; // Random moving distraction
    [SerializeField] RandomPickerChanting chantingDist; // Random chanting distraction
    [SerializeField] int maxNoOfDistractionLevels = 3; // Maximum number of distraction levels
    [SerializeField] DistractionLevelData distractionLevelData; // Data for distraction settings
    [SerializeField] GameObject net; // Game net object
    [SerializeField] GameObject netCollider; // Collider for the net
    [SerializeField] GameObject[] levelsOfDistractions; // Array of distraction levels

    int index = 0; // Current distraction index
    int csvIndex; // Index for CSV logging
    float _timer; // Timer to control distractions

    public bool distractionsWithAudio; // Flag for audio-enabled distractions
    bool isPlayingDistarction; // Whether a distraction is currently active
    bool isReadyForDistraction; // Whether ready for the next distraction

    public Action<string> OnDistarctionStart; // Event triggered when distraction starts
    public Action OnDistarctionEnd; // Event triggered when distraction ends

    public int DistractionIndex => index; // Getter for the current distraction index

    private void OnEnable()
    {
        if (distractionLevelData != null)
        {
            // Set distractions with audio based on data
            distractionsWithAudio = distractionLevelData.withAudio;

            // Set the maximum number of distraction levels
            int maxDists = (int)distractionLevelData.distractionLevel;
            maxNoOfDistractionLevels = maxDists;

            // Activate the defined distraction levels
            for (int i = 0; i < maxNoOfDistractionLevels; i++)
            {
                levelsOfDistractions[i].SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        OnDistarctionStart -= DebugDistractionStart;
        OnDistarctionEnd -= DebugDistractionEnd;
        EventsManager.OnEndDistraction -= ResetTimer;
    }

    // Checks if distractions are selective
    public bool IsSelective()
    {
        return distractionLevelData.distractionLevel != DistractionLevel.None && !distractionLevelData.Adaptive;
    }

    // Checks if distractions are adaptive
    public bool IsAdaptive()
    {
        return distractionLevelData.distractionLevel != DistractionLevel.None && distractionLevelData.Adaptive;
    }

    private void Update()
    {
        if (isPlayingDistarction) return; // Skip if a distraction is already playing
        if (!IsSelective() && !IsAdaptive()) return; // Skip if no distractions are active

        _timer += Time.deltaTime; // Increment timer

        if (_timer > timeToPlayNextDistract) // Check if it's time for the next distraction
        {
            if (IsAdaptive())
            {
                // Destroy the ball if adaptive distractions are enabled
                Ball ball = FindObjectOfType<Ball>();
                if (ball != null)
                    Destroy(ball.gameObject);
            }

            EventsManager.OnGamePause?.Invoke(); // Pause the game
            index++; // Increment the distraction index
            _timer = 0; // Reset the timer
            HandleDistraction(); // Handle the current distraction

            csvIndex = index; // Update the CSV index

            if (index >= maxNoOfDistractionLevels) // Reset index if it exceeds the maximum
                index = 0;

            isReadyForDistraction = false; // Reset readiness for distraction
        }
    }

    // Prepares for the next distraction
    public void OnGetReadyForDistraction()
    {
        isReadyForDistraction = true;
    }

    // Handles the current distraction based on the index
    public void HandleDistraction()
    {
        if (index == 1)
        {
            if (!levelsOfDistractions[index - 1].activeInHierarchy) return;
            
            chantingDist.MakeRandomCharacterChant(); // Trigger chanting distraction
            OnDistarctionStart?.Invoke("Boy Waving");
            AudioManager.Singleton.PlaySFXBasedOnSO("wavetopeople"); // Play audio
            EventsManager.OnPlayDistraction?.Invoke();
        }
        else if (index == 2)
        {
            if (!levelsOfDistractions[index - 1].activeInHierarchy) return;
            
            movingDist.MakeRandomCharacterMove(); // Trigger moving distraction
            OnDistarctionStart?.Invoke("Referee Running");
            AudioManager.Singleton.PlaySFXBasedOnSO("wavetocoach");
            EventsManager.OnPlayDistraction?.Invoke();
        }
        else if (index == 3)
        {
            if (!levelsOfDistractions[index - 1].activeInHierarchy) return;
            
            birdDist.PlayBirdVFX(); // Trigger bird visual distraction
            OnDistarctionStart?.Invoke("Birds");
            AudioManager.Singleton.PlaySFXBasedOnSO("wavetobirds");
            EventsManager.OnPlayDistraction?.Invoke();
        }
    }

    // Resets the timer
    void ResetTimer()
    {
        _timer = 0;
    }

    // Logs when a distraction starts
    void DebugDistractionStart(string distractionName)
    {
        Debug.Log("Start Distract");
        isPlayingDistarction = true;
    }

    // Logs when a distraction ends
    void DebugDistractionEnd()
    {
        Debug.Log("End Distract");
        isPlayingDistarction = false;
        EventsManager.OnGameResume?.Invoke(); // Resume the game
    }
}
