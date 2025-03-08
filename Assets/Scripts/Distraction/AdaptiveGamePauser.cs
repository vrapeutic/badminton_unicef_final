using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveGamePauser : MonoBehaviour
{
    #region Singleton

    public static AdaptiveGamePauser Instance; // Singleton instance for global access

    private void Awake()
    {
        // Initialize the Singleton instance or destroy duplicates
        if (Instance == null)
            Instance = this;
        else
        {
            if (Instance != this)
                Destroy(gameObject); // Destroy duplicate instances to enforce the Singleton pattern
        }
    }

    #endregion

    [SerializeField] Collider _collider; // Collider used to manage interactions during pause
    [SerializeField] DistractionLevelData _distractionLevelData; // Data object to determine if adaptive distractions are active
    [SerializeField] GameObject shakeHandObject; // Reference to an object related to the gameplay (e.g., for distractions)

    [SerializeField] Animator[] NPCs; // Array of NPCs' animators to control their behavior during pause/resume

    public bool isPaused; // Tracks whether the game is currently paused

    private void Start()
    {
        // Subscribe to game pause and resume events
        EventsManager.OnGamePause += Pausegame;
        EventsManager.OnGameResume += ResumeGame;
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        EventsManager.OnGamePause -= Pausegame;
        EventsManager.OnGameResume -= ResumeGame;
    }

    // Pauses the game and NPCs' animations
    [ContextMenu("Pause")]
    void Pausegame()
    {
        if (!_distractionLevelData.Adaptive) return; // Exit if adaptive distractions are not enabled

        _collider.enabled = true; // Enable the collider for interactions
        AudioManager.Singleton.PlaySFX((int)SFX.SFX_Slow); // Play a slow-motion sound effect

        // Stop NPC animations by setting their speed to zero
        for (int i = 0; i < NPCs.Length; i++)
        {
            NPCs[i].speed = 0;
        }

        isPaused = true; // Mark the game as paused
    }

    // Resumes the game and NPCs' animations
    [ContextMenu("resume")]
    void ResumeGame()
    {
        if (!_distractionLevelData.Adaptive) return; // Exit if adaptive distractions are not enabled

        // Resume NPC animations by setting their speed back to one
        for (int i = 0; i < NPCs.Length; i++)
        {
            NPCs[i].speed = 1;
        }

        isPaused = false; // Mark the game as unpaused
    }
}
