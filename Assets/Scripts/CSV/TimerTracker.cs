using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTracker : MonoBehaviour
{
    // Variables to track time for selective and sustained attention tasks
    public DateTime targetStart; // The time when the target starts
    public float startTime; // The time in seconds since the game started when the target starts
    public DateTime targetHit; // The time when the target is hit
    public float hitTime; // The time in seconds since the game started when the target is hit
    public float interruptionDuration; // The duration of the interruption between start and hit times

    // Variables for adaptive tasks
    public string distractorName; // The name of the distractor (if any)
    public float timeFollowingIt; // The time following the distractor's appearance

    // Sets the starting time for an event
    public void SetStartTime()
    {
        startTime = Time.realtimeSinceStartup; // Record the real-time since the startup
        targetStart = DateTime.Now; // Record the current date and time
    }

    // Sets the hitting time for an event
    public void SetHitTime()
    {
        hitTime = Time.realtimeSinceStartup; // Record the real-time since the startup
        targetHit = DateTime.Now; // Record the current date and time
    }

    // Calculates and sets the duration of an interruption
    public void SetInterruptionDuration()
    {
        SetHitTime(); // Update the hit time
        interruptionDuration = hitTime - startTime; // Calculate the duration as the difference between hit and start times
    }

    // Reverts the interruption duration to a placeholder value
    public void RevertInterruptDuration()
    {
        SetHitTime(); // Update the hit time
        interruptionDuration = -999; // Assign a placeholder value for interruption duration
    }

    // Records data when a correct hit is made
    public void SetDataOnHit()
    {
        SetInterruptionDuration(); // Calculate the interruption duration
        CsvReadWrite.Instance.WriteNonAdaptiveDataRaw(
            targetStart.ToString(), // Record the start time as a string
            targetHit.ToString(), // Record the hit time as a string
            interruptionDuration.ToString() // Record the interruption duration as a string
        );
    }

    // Records data for a wrong hit
    public void SetWrongHitDataOnHit()
    {
        RevertInterruptDuration(); // Revert the interruption duration
        CsvReadWrite.Instance.WriteNonAdaptiveDataRaw(
            targetStart.ToString(), // Record the start time as a string
            targetHit.ToString(), // Record the hit time as a string
            interruptionDuration.ToString() // Record the interruption duration as a string
        );
    }
}
