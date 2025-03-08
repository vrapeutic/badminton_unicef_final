using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem birds; // Reference to the ParticleSystem for the bird visual effects
    [SerializeField] BirdsColliderController birdsColliderController; // Reference to the BirdsColliderController for managing collider interactions

    // Method to play bird-related visual effects and audio
    [ContextMenu("Play")]
    public void PlayBirdVFX()
    {
        // Uncomment the line below to play the bird ParticleSystem if needed
        // birds.Play();

        // Trigger the movement of the bird collider using the BirdsColliderController
        birdsColliderController.MoveCollider();

        // Check if distractions with audio are enabled, then play the bird sound effect
        if (DistractionManager.Singleton.distractionsWithAudio)
            AudioManager.Singleton.PlaySFX((int)SFX.SFX_Birds);
    }
}