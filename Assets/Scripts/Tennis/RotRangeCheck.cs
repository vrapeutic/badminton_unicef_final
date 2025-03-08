using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotRangeCheck : MonoBehaviour
{
    public bool isRotCorrect; // Boolean to check if the rotation is correct
    [SerializeField] BallShooter ballShooter; // Reference to the BallShooter component
    [SerializeField] bool flip; // Determines if the force should be flipped (not utilized in this script)
    [SerializeField] float forceMultiplier = 10f; // Multiplier for the shooting force (not utilized in this script)
    [SerializeField] Transform targetShoot; // Reference to the target shooting position
    [SerializeField] private bool alreadyhit; // Tracks whether the object has already been hit
    public bool AlreadyHit => alreadyhit; // Public getter for the `alreadyhit` variable

    // Called when a collision with another object occurs
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the Ball component
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null && !AlreadyHit) // If it is a Ball and not already hit
        {
            Debug.Log("Collision"); // Log the collision
            alreadyhit = true; // Mark as hit
            ball.HandleBallCorrectHit(); // Call the correct hit handler on the Ball
            LeanTween.cancel(gameObject); // Cancel any active LeanTween animations on this object
            
            // Calculate the rotated direction vector
            float angle = -30.0f;
            Quaternion rotation = Quaternion.Euler(angle, 0, 0); // Rotate around the X-axis
            Vector3 rotatedVector = rotation * Vector3.forward; // Apply the rotation to the forward vector
            
            // Shoot the ball in the rotated direction
            ballShooter.ShootInDirection(rotatedVector);
            
            // Enable all colliders attached to this object
            Collider[] _colliders = GetComponents<Collider>();

            foreach (Collider col in _colliders)
            {
                col.enabled = true; // Enable the collider
            }
            
            // Set a delayed call to reset the `alreadyhit` flag and re-enable colliders
            LeanTween.delayedCall(gameObject, 1f, () =>
            {
                alreadyhit = false; // Reset the hit flag
                Collider[] colliders = GetComponents<Collider>();

                foreach (Collider col in colliders)
                {
                    col.enabled = true; // Re-enable the collider
                }
            });
        }
    }
}
