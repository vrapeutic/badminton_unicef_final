using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWavingCheck : MonoBehaviour
{
    // Triggered when another collider enters this GameObject's collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is named "Left Direct Interactor" or "Right Direct Interactor"
        if (other.gameObject.name == "Left Direct Interactor" || other.gameObject.name == "Right Direct Interactor")
        {
            // Invoke the end of distraction events in both DistractionManager and EventsManager
            DistractionManager.Singleton.OnDistarctionEnd?.Invoke();
            EventsManager.OnEndDistraction?.Invoke();

            // Log that the waving action is completed
            Debug.Log("DoneWaving");

            // Play the correct sound effect using the AudioManager
            AudioManager.Singleton.PlaySFX((int)SFX.SFX_Correct);

            // Deactivate this GameObject
            gameObject.SetActive(false);
        }
    }
}