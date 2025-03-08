using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistractionUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text subtitleText; // Reference to the UI text component for displaying subtitles

    private void Start()
    {
        // Subscribe to events when the script starts
        EventsManager.OnSendSubtitleUI += SetSubtitleText; // Update subtitle text
        EventsManager.OnPlayDistraction += ShowUI; // Show the UI during a distraction
        EventsManager.OnEndDistraction += HideUI; // Hide the UI when the distraction ends
    }

    private void OnDestroy()
    {
        // Unsubscribe from events when the script is destroyed to prevent memory leaks
        EventsManager.OnSendSubtitleUI -= SetSubtitleText;
        EventsManager.OnPlayDistraction -= ShowUI;
        EventsManager.OnEndDistraction -= HideUI;
    }

    // Updates the subtitle text with the provided string
    public void SetSubtitleText(string subtileString)
    {
        subtitleText.text = subtileString; // Set the UI text to the provided subtitle string
    }

    // Displays the UI if the distraction mode is not selective
    private void ShowUI()
    {
        if (DistractionManager.Singleton.IsSelective()) return; // Do nothing if in selective distraction mode
        transform.GetChild(0).gameObject.SetActive(true); // Activate the first child of the GameObject
    }

    // Hides the UI
    private void HideUI()
    {
        transform.GetChild(0).gameObject.SetActive(false); // Deactivate the first child of the GameObject
    }
}