using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameEvent correctHitEvent; // Event triggered when the ball is correctly hit
    [SerializeField] GameEvent wallHitEvent; // Event triggered when the ball hits a wall
    [SerializeField] GameEvent startTimeEvent; // Event triggered when the ball enters the player area
    [SerializeField] Rigidbody rb; // Rigidbody component of the ball
    [SerializeField] GameObject ballVisual_1; // Visual representation for one side of the ball
    [SerializeField] GameObject ballVisual_2; // Visual representation for the other side of the ball
    [SerializeField] private float maxTimeAlive = 25f; // Maximum time the ball can stay active
    [SerializeField] private Collider ballCollider; // Collider component of the ball
    
    private float timeAlive; // Timer to track how long the ball has been active
    Vector3 cachedVelocity; // Cached velocity for pausing and resuming the ball
    private bool canRegisterHits; // Flag to determine if the ball can register hits
    
    private void OnEnable()
    {
        timeAlive = 0; // Reset the time alive
        canRegisterHits = true; // Allow hit registration
        Invoke("EnableBallCollider", 1f); // Delay enabling the ball collider
    }

    private void Start()
    {
        // Assign the ball as the target for the BallFollower
        BallFollower ballFollower = GameObject.FindObjectOfType<BallFollower>();
        ballFollower.targetToFollow = this.gameObject.transform;

        // Subscribe to events
        EventsManager.OnGamePause += StopBall;
        EventsManager.OnGameResume += ContinueBall;
        EventsManager.OnWinEvent += RemovefromScene;
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        EventsManager.OnGamePause -= StopBall;
        EventsManager.OnGameResume -= ContinueBall;
        EventsManager.OnWinEvent -= RemovefromScene;
    }

    private void Update()
    {
        // Increment the time alive
        timeAlive += Time.deltaTime;
        if (timeAlive > maxTimeAlive) // Check if the ball's lifetime has expired
        {
            RemovefromScene();
        }
        
        // Update ball visuals based on its velocity
        if (rb != null)
        {
            if (rb.velocity.z > 0)
            {
                ballVisual_1.SetActive(true);
                ballVisual_2.SetActive(false);
            }
            else
            {
                ballVisual_1.SetActive(false);
                ballVisual_2.SetActive(true);
            }
        }
    }

    // Enables the ball collider after a delay
    private void EnableBallCollider()
    {
        ballCollider.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "GroundCollider") // Check for collision with the ground
        {
            AudioManager.Singleton.PlaySFX((int)SFX.SFX_BallHit); // Play ball hit sound effect
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boxes")) // Check for collision with objects tagged as "Boxes"
        {
            GameController.Instance.OnBallCollide?.Invoke(); // Invoke the ball collision event
            if (canRegisterHits)
            {
                wallHitEvent.Raise(); // Raise the wall hit event
            }
            Debug.Log("Miss");
            canRegisterHits = false; // Disable hit registration
            Destroy(gameObject); // Destroy the ball
        }

        if (other.gameObject.name == "PlayerArea") // Check if the ball enters the player area
        {
            startTimeEvent.Raise(); // Raise the start time event
        }
    }

    // Handles the logic for a correct hit
    public void HandleBallCorrectHit()
    {
        canRegisterHits = false; // Disable hit registration
        AudioManager.Singleton.PlaySFX((int)SFX.SFX_BallHit); // Play ball hit sound effect
        Debug.Log("Hit Racket");
        GameController.Instance.IncreaseNumberOfHits(); // Increase the player's hit count
        correctHitEvent.Raise(); // Raise the correct hit event
        Destroy(gameObject); // Destroy the ball
    }

    // Sets the availability of collision detection
    public void SetCollisionAvailability(bool isOn)
    {
        canRegisterHits = isOn;
    }

    // Removes the ball from the scene
    public void RemovefromScene()
    {
        GameObject.Destroy(gameObject); // Destroy the ball
        DistractionManager.Singleton.OnGetReadyForDistraction(); // Notify the distraction manager
    }

    // Stops the ball by caching its velocity and setting it to kinematic
    public void StopBall()
    {
        if (DistractionManager.Singleton.IsAdaptive())
        {
            cachedVelocity = rb.velocity; // Cache the current velocity
            rb.isKinematic = true; // Stop the ball
            Debug.Log("Stop ball");
        }
    }

    // Resumes the ball's movement using the cached velocity
    public void ContinueBall()
    {
        if (DistractionManager.Singleton.IsAdaptive() && rb.isKinematic)
        {
            rb.velocity = cachedVelocity; // Restore the cached velocity
            rb.isKinematic = false; // Resume the ball's physics
            Debug.Log("continue ball");
        }
    }

    // Gets the normalized velocity direction of the ball
    public Vector3 GetVelocityDirection()
    {
        return rb.velocity.normalized;
    }
    
    // Draws a debug ray in the direction of the ball's velocity
    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, rb.velocity.normalized * 2f, Color.green);
    }
}