using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OnButtonInteractionUI : MonoBehaviour
{
    [SerializeField] Color idleColor; // The color of the text when the button is idle
    [SerializeField] Color selectColor; // The color of the text when the button is selected

    [SerializeField] TMP_Text targetText; // Reference to the TextMeshPro text component to update the color

    // Subscribes to the OnButtonClicked event when the script starts
    private void Start()
    {
        EventsManager.OnButtonClicked += OnIdleInteract;
    }

    // Unsubscribes from the OnButtonClicked event when the script is destroyed
    private void OnDestroy()
    {
        EventsManager.OnButtonClicked -= OnIdleInteract;
    }

    // Method triggered when the button is clicked
    public void OnInteractClick()
    {
        targetText.color = selectColor; // Change the text color to the "selected" color
        EventsManager.OnButtonClicked?.Invoke(this); // Invoke the OnButtonClicked event with this instance
        AudioManager.Singleton.PlaySFX((int)SFX.SFX_ButtonClick); // Play the button click sound effect
    }

    // Method triggered by the OnButtonClicked event to reset the text color if the button is idle
    void OnIdleInteract(OnButtonInteractionUI onButtonInteractionUI)
    {
        if (onButtonInteractionUI == this) return; // Ignore the event if it was triggered by this button
        targetText.color = idleColor; // Reset the text color to the "idle" color
    }
}