using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionTimeTracking : MonoBehaviour
{
    #region Singleton

    public static DistractionTimeTracking Singleton; // Singleton instance for global access

    private void Awake()
    {
        // Ensure a single instance of this class
        if (Singleton == null)
            Singleton = this;
        else
        {
            if (Singleton != this)
                Destroy(gameObject); // Destroy duplicate instances to enforce the Singleton pattern
        }
    }

    #endregion

    [SerializeField] float adaptiveFollowingTime; // Tracks the total time spent following adaptive distractions
    [SerializeField] float collectiveTime; // Tracks the total time spent on selective distractions
    public float CollectiveTime => collectiveTime; // Public property to access `collectiveTime`

    bool canTrackAdaptiveTime; // Flag indicating whether to track adaptive distraction time
    bool alreadyEnded; // Flag indicating whether the distraction has ended
    public bool AlreadyEnded => alreadyEnded; // Public property to access `alreadyEnded`

    string distractionName; // Name of the current distraction

    private void OnEnable()
    {
        // Subscribe to distraction start and end events
        DistractionManager.Singleton.OnDistarctionStart += SetupDistraction;
        DistractionManager.Singleton.OnDistarctionEnd += SendDataToCSVWriter;
    }

    private void OnDisable()
    {
        // Unsubscribe from distraction start and end events
        DistractionManager.Singleton.OnDistarctionStart -= SetupDistraction;
        DistractionManager.Singleton.OnDistarctionEnd -= SendDataToCSVWriter;
    }

    private void Update()
    {
        if (alreadyEnded) return; // Exit if the distraction has already ended

        // Increment collective time if it's a selective distraction and the head sensor detects it
        if (DistractionManager.Singleton.IsSelective() && HeadSensor.Singleton.IsDetectingDistraction)
        {
            collectiveTime += Time.deltaTime;
        }

        // Increment adaptive following time if it's an adaptive distraction and tracking is enabled
        if (DistractionManager.Singleton.IsAdaptive() && canTrackAdaptiveTime)
        {
            adaptiveFollowingTime += Time.deltaTime;
        }
    }

    // Resets adaptive following time and enables tracking
    public void SetCanTrackAdaptiveTime()
    {
        adaptiveFollowingTime = 0; // Reset adaptive time
        canTrackAdaptiveTime = true; // Enable tracking
    }

    // Sets up distraction state when a distraction starts
    void SetupDistraction(string _distractionName)
    {
        alreadyEnded = false; // Reset the `alreadyEnded` flag
        distractionName = _distractionName; // Store the name of the current distraction

        // If it's an adaptive distraction, enable adaptive time tracking
        if (DistractionManager.Singleton.IsAdaptive())
        {
            SetCanTrackAdaptiveTime();
        }
    }

    // Sends distraction data to the CSV writer and resets states
    void SendDataToCSVWriter()
    {
        if (alreadyEnded) return; // Skip if the distraction has already ended

        // Handle selective distractions
        if (DistractionManager.Singleton.IsSelective())
        {
            CsvReadWrite.Instance.WriteDistarctionData(distractionName, collectiveTime.ToString()); // Write data to CSV
            collectiveTime = 0; // Reset collective time
        }

        // Handle adaptive distractions
        if (DistractionManager.Singleton.IsAdaptive())
        {
            CsvReadWrite.Instance.WriteDistarctionData(distractionName, adaptiveFollowingTime.ToString()); // Write data to CSV
            adaptiveFollowingTime = 0; // Reset adaptive time
            canTrackAdaptiveTime = false; // Disable tracking
        }

        alreadyEnded = true; // Mark the distraction as ended
    }
}
