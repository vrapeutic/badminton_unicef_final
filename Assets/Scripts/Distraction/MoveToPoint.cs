using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MoveToPoint : MonoBehaviour
{
    [SerializeField] Transform[] targets; // List of target points for the NavMeshAgent to navigate to
    [SerializeField] int currentTarget = 0; // Index of the current target
    [SerializeField] bool isMoving; // Tracks whether the object is currently moving
    [SerializeField] NavMeshAgent agent; // NavMeshAgent for pathfinding
    [SerializeField] Animator animator; // Animator to control character animations
    [SerializeField] Collider _collider; // Collider associated with the object
    [SerializeField] GameObject _fx; // Visual effect to enable during movement

    private bool arrived = false; // Tracks if the agent has arrived at its destination
    private bool playerWaved; // Tracks if the player has waved back during distraction

    private void Start()
    {
        // Subscribe to the OnEndDistraction event to handle waving back to the player
        EventsManager.OnEndDistraction += WaveBackToPlayer;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnEndDistraction event to prevent memory leaks
        EventsManager.OnEndDistraction -= WaveBackToPlayer;
    }

    private void Update()
    {
        // Check if the agent has reached close to the current target
        if (Vector3.Distance(transform.position, targets[currentTarget].position) < 0.5f)
        {
            arrived = true;
        }
        else
        {
            arrived = false;
        }

        // Update the animator based on arrival status
        animator.SetBool("Arrived", arrived);

        if (arrived && isMoving)
        {
            // Disable collider and visual effects when destination is reached
            _collider.enabled = false;
            _fx.SetActive(false);

            // Signal the end of the distraction
            DistractionManager.Singleton.OnDistarctionEnd?.Invoke();

            isMoving = false; // Stop movement
            animator.ResetTrigger("Run");
            animator.SetTrigger("Idle");

            // Rotate the object 180 degrees
            LeanTween.rotateAround(gameObject, transform.up, 180f, 1f);
        }
    }

    // Change the destination to the next target
    [ContextMenu("ChangeDest")]
    public void ChangeDestination()
    {
        if (isMoving) return; // Do nothing if already moving

        _collider.enabled = true; // Enable the collider
        _fx.SetActive(true); // Activate visual effects
        playerWaved = false; // Reset player waved status

        // Move to the next target in the list
        currentTarget++;

        if (currentTarget >= targets.Length)
        {
            currentTarget = 0; // Loop back to the first target
        }

        // Update animator to play running animation
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Run");

        // Set the agent's destination to the next target
        agent.destination = targets[currentTarget].position;

        // Start coroutine to manage pause behavior
        StartCoroutine(WaitToPause());

        isMoving = true; // Mark as moving
    }

    // Coroutine to handle pausing and distraction timing
    IEnumerator WaitToPause()
    {
        if (DistractionManager.Singleton.IsAdaptive())
            EventsManager.OnGamePause?.Invoke(); // Signal game pause if adaptive mode is active

        yield return new WaitForSeconds(4f); // Wait for 4 seconds
        StopCoroutine(WaitToPause());

        if (playerWaved)
            EventsManager.OnEndDistraction?.Invoke(); // Signal the end of the distraction if the player waved

        DistractionManager.Singleton.OnDistarctionEnd?.Invoke(); // Notify the DistractionManager
    }

    // Handles waving back to the player
    private void WaveBackToPlayer()
    {
        if (DistractionManager.Singleton.DistractionIndex != 2) return; // Exit if not the correct distraction index

        animator.CrossFadeInFixedTime("Waving", 0.2f); // Trigger waving animation
        agent.speed = 0; // Stop the NavMeshAgent

        // Save the original direction of the agent
        Vector3 originalDirection = agent.transform.forward;

        // Rotate the agent to face the player
        Vector3 playerDirection = Camera.main.transform.position - transform.position;
        Quaternion rotationToPlayer = Quaternion.LookRotation(playerDirection);
        float yRotation = rotationToPlayer.eulerAngles.y;

        playerWaved = true; // Mark player as having waved

        // Rotate to face the player
        LeanTween.rotateY(gameObject, yRotation, 1f);

        // Delay and then rotate back to the original direction
        LeanTween.delayedCall(gameObject, 5f, () =>
        {
            Quaternion rotationToOrigin = Quaternion.LookRotation(originalDirection);
            float yRotationToOrigin = rotationToOrigin.eulerAngles.y;
            LeanTween.rotateY(gameObject, yRotationToOrigin, 1f);

            agent.speed = 3.5f; // Reset the NavMeshAgent's speed
        });
    }
}