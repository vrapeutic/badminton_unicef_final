using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickerMoving : MonoBehaviour
{
    [SerializeField] MoveToPoint[] movingCharacters; // Array of characters capable of moving to specified points

    int currentMovingCharacter; // Tracks the index of the current moving character

    // Method to make a character from the array move to a new destination
    public void MakeRandomCharacterMove()
    {
        // Check if the GameObject this script is attached to is active in the hierarchy
        if (this.gameObject.activeInHierarchy)
        {
            currentMovingCharacter++; // Increment the index to pick the next character

            // Reset the index if it exceeds the array length
            if (currentMovingCharacter >= movingCharacters.Length)
            {
                currentMovingCharacter = 0; // Wrap around to the first character in the array
            }

            // Alternative option commented out for random selection
            // int randIndex = Random.Range(0, movingCharacters.Length);

            // Instruct the selected character to change its destination
            movingCharacters[currentMovingCharacter].ChangeDestination();
        }
    }
}