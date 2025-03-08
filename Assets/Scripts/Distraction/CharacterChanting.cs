using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterChanting : MonoBehaviour
{
    [SerializeField] Animator animator; // Animator component to control character animations
    [SerializeField] GameObject VFX; // GameObject for visual effects (VFX)
    [SerializeField] Collider _collider; // Collider associated with this character for interactions

    // Method called when the script is enabled
    private void OnEnable()
    {
        StopVFX(); // Stop any active visual effects when the character is enabled
    }

    private void Start()
    {
        // Subscribe to the OnGameResume event to stop VFX and disable the collider
        EventsManager.OnGameResume += StopVFX;
        EventsManager.OnGameResume += DisableCollider;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnGameResume event to prevent memory leaks
        EventsManager.OnGameResume -= StopVFX;
        EventsManager.OnGameResume -= DisableCollider;
    }

    // Public method triggered to start the chanting sequence
    [ContextMenu("Chant")]
    public void DoChanting()
    {
        animator.SetBool("Wave", true); // Trigger the waving animation
        PlayVFX(); // Activate visual effects
        EnableCollider(); // Enable the collider for interactions

        // Start a coroutine to manage chanting duration and pause behavior
        StartCoroutine(WaitToPause());
    }

    // Activates the visual effects (VFX)
    void PlayVFX()
    {
        VFX.SetActive(true); // Set the VFX GameObject to active
    }

    // Stops the visual effects and resets animation
    [ContextMenu("StopVFX")]
    void StopVFX()
    {
        VFX.SetActive(false); // Deactivate the VFX GameObject
        animator.SetBool("Wave", false); // Reset the waving animation state
    }

    // Enables the collider for interactions
    void EnableCollider()
    {
        _collider.enabled = true; // Enable the collider
    }

    // Disables the collider to prevent further interactions
    void DisableCollider()
    {
        _collider.enabled = false; // Disable the collider
    }

    // Coroutine to handle pause behavior and wait for a defined duration
    IEnumerator WaitToPause()
    {
        // Trigger the game pause event if adaptive distractions are enabled
        if (DistractionManager.Singleton.IsAdaptive())
            EventsManager.OnGamePause?.Invoke();

        yield return new WaitForSeconds(10f); // Wait for 10 seconds

        StopCoroutine(WaitToPause()); // Stop the coroutine
        EventsManager.OnEndDistraction?.Invoke(); // Notify the end of distraction event
        DistractionManager.Singleton.OnDistarctionEnd?.Invoke(); // Signal the distraction manager
    }
}