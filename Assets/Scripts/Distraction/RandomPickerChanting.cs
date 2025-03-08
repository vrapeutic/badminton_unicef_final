using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickerChanting : MonoBehaviour
{
    [SerializeField] CharacterChanting[] chantingCharacters; // Array of characters available to perform chanting

    // Method to make a random character perform chanting
    public void MakeRandomCharacterChant()
    {
        // Check if this GameObject is active in the hierarchy
        if (this.gameObject.activeInHierarchy)
        {
            // Select a random character from the array
            int randIndex = Random.Range(0, chantingCharacters.Length);
            
            // Trigger the chanting action for the randomly selected character
            chantingCharacters[randIndex].DoChanting();

            // Play an audio cue if distractions with audio are enabled
            if (DistractionManager.Singleton.distractionsWithAudio)
                AudioManager.Singleton.PlaySFX((int)SFX.SFX_Hey);
        }
    }    
}