using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSensor : MonoBehaviour
{
    #region Singleton

    public static HeadSensor Singleton; // Singleton instance for global access

    private void Awake()
    {
        // Ensure there is only one active instance of this script
        if (Singleton == null)
            Singleton = this;
        else
        {
            if (Singleton != this)
                Destroy(gameObject); // Destroy duplicate instances to enforce Singleton pattern
        }
    }

    #endregion

    [SerializeField] LayerMask layerMask; // Defines the layers that the Raycast should detect
    [SerializeField] GameObject targetToWave; // Reference to the object that will become active upon detecting a distraction

    bool isDetectingDistraction; // Tracks whether a distraction is being detected

    public bool IsDetectingDistraction => isDetectingDistraction; // Public getter for checking distraction detection status

    void Update()
    {
        // Draw a debug ray in the Scene view for visualization purposes
        Debug.DrawRay(transform.position, transform.forward * 20, Color.green);

        RaycastHit hit; // Stores information about the object hit by the Raycast
        // Cast a Ray forward from the sensor's position to check for collisions within the specified layerMask
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, layerMask))
        {
            Debug.Log(hit.collider.gameObject.name); // Log the name of the object hit

            isDetectingDistraction = true; // Set distraction detection to true

            // If the distraction is adaptive and has not already ended, activate the target object
            if (DistractionManager.Singleton.IsAdaptive() && !DistractionTimeTracking.Singleton.AlreadyEnded)
            {
                targetToWave.SetActive(true); // Activate the target object (e.g., prompt for user attention)
            }
        }
        else
        {
            isDetectingDistraction = false; // Set distraction detection to false when nothing is hit
        }
    }
}
