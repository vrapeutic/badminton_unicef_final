using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBox : MonoBehaviour
{
    [SerializeField] Collider _collider; // Reference to the Collider component that will be activated

    // Triggered when another object exits the trigger collider attached to this GameObject
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger has a Ball component
        if (other.gameObject.GetComponent<Ball>())
        {
            _collider.enabled = true; // Enable the collider when the Ball exits
        }
    }
}