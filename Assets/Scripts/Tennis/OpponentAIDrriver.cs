using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAIDrriver : MonoBehaviour
{
    [SerializeField] Transform targetPos; // Position the AI should move towards when a ball is present
    [SerializeField] Transform midTargetPos; // Default position to move towards when no ball is present
    [SerializeField] Animator animator; // Animator for controlling AI animations
    [SerializeField] BallShooter ballShooter; // Reference to the BallShooter script
    [SerializeField] Collider _collider; // Collider for detecting interactions
    [SerializeField] Collider _SeparatorCollider; // Collider that separates play zones
    [SerializeField] float speed; // Speed at which the AI moves
    [SerializeField] float refreshRate; // Time interval for refreshing AI states
    [SerializeField] GameEvent targetStartGameEvent; // Event triggered when AI strikes the ball
    public Transform targetball; // Transform of the ball the AI is targeting

    GameObject ballRef; // Reference to the current ball
    float _timer; // Timer for refreshing AI states

    bool left; // Tracks if AI is moving left
    bool right; // Tracks if AI is moving right
    [SerializeField] bool strike; // Tracks if AI is striking the ball
    bool idle; // Tracks if AI is idle

    public bool Strike => strike; // Public getter for `strike`

    void Start()
    {
        // Subscribe to the OnBallCollide event to reset the target ball
        GameController.Instance.OnBallCollide += ResetTargetBall;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnBallCollide event
        GameController.Instance.OnBallCollide -= ResetTargetBall;
    }

    void Update()
    {
        if (strike) return; // If the AI is striking, skip the update logic

        Transform currentTarget = midTargetPos; // Default target position

        if (targetball == null)
        {
            // Move towards the midTargetPos if no ball is present
            currentTarget = midTargetPos;
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        }
        else
        {
            // Move towards the target ball if present
            currentTarget = targetPos;
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        }

        _timer += Time.deltaTime; // Increment the timer
        if (_timer < refreshRate) return; // Skip further logic if the timer hasn't reached the refresh rate
        else
        {
            _timer = 0; // Reset the timer
        }

        bool idleCheck = false;

        // Check if the AI should switch to idle
        idleCheck = (((transform.position.x - currentTarget.position.x) < 0.5 && (transform.position.x - currentTarget.position.x) > 0) || 
                     ((transform.position.x - currentTarget.position.x) > -0.5 && (transform.position.x - currentTarget.position.x) < 0)) && 
                    !idle;

        // Handle movement animations based on position differences
        if ((transform.position.x - currentTarget.position.x) < 0 && !right && targetball)
        {
            animator.ResetTrigger("GoRight");
            animator.ResetTrigger("Strike");
            animator.ResetTrigger("idle");
            animator.SetTrigger("GoLeft");
            ResetLeft();
            ResetRight();
            ResetIdle();
            right = true;
        }
        else if ((transform.position.x - currentTarget.position.x) > 0.5f && !left && targetball)
        {
            animator.ResetTrigger("Strike");
            animator.ResetTrigger("GoLeft");
            animator.ResetTrigger("idle");
            animator.SetTrigger("GoRight");
            ResetLeft();
            ResetRight();
            ResetIdle();
            left = true;
        }
        else if (idleCheck && !strike)
        {
            GoToIdle(); // Switch to idle if conditions are met
        }
    }

    // Resets the target ball
    void ResetTargetBall()
    {
        targetball = null;
    }

    // Resets the `left` flag
    public void ResetLeft()
    {
        left = false;
    }

    // Resets the `right` flag
    public void ResetRight()
    {
        right = false;
    }

    // Resets the `strike` state
    public void ResetStrike()
    {
        _collider.enabled = true;
        strike = false;
    }

    // Resets the `idle` state
    public void ResetIdle()
    {
        idle = false;
    }

    // Switches the AI to idle mode
    public void GoToIdle()
    {
        animator.ResetTrigger("GoRight");
        animator.ResetTrigger("GoLeft");
        animator.ResetTrigger("Strike");
        animator.SetTrigger("idle");
        ResetLeft();
        ResetRight();
        ResetIdle();
        idle = true;
    }

    // Handles ball striking
    public void StrikeBall()
    {
        ballShooter.Shoot(); // Trigger the ball shooter to shoot

        if (targetStartGameEvent)
            targetStartGameEvent.Raise(); // Raise the event if it's assigned
    }

    // Handles custom ball striking logic
    public void CustomStrikeBall()
    {
        strike = true;
        animator.ResetTrigger("GoRight");
        animator.ResetTrigger("GoLeft");
        animator.ResetTrigger("idle");
        animator.SetTrigger("Strike");
        ResetLeft();
        ResetRight();
        ResetIdle();
    }

    // Removes the ball from the scene
    public void RemoveBall()
    {
        if (ballShooter != null && ballRef != null)
            ballRef.GetComponent<Ball>().RemovefromScene();
    }

    // Disables the AI collider
    public void DisableCollision()
    {
        _collider.enabled = false;
    }

    // Enables the AI collider
    public void EnableCollision()
    {
        _collider.enabled = true;
    }

    // Executes actions when the AI touches a ball
    public void ActionOnBallTouch(GameObject newBall)
    {
        strike = true;
        animator.ResetTrigger("GoRight");
        animator.ResetTrigger("GoLeft");
        animator.ResetTrigger("idle");
        animator.SetTrigger("Strike");
        ResetLeft();
        ResetRight();
        ResetIdle();

        _collider.enabled = false; // Disable the collider during interaction
        ballRef = newBall; // Store a reference to the new ball
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _SeparatorCollider)
        {
            targetball = null; // Reset the target ball if it enters the separator
            GoToIdle(); // Switch to idle state
        }
    }
}
