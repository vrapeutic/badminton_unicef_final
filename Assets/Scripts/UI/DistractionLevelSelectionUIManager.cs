using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistractionLevelSelectionUIManager : MonoBehaviour
{
    [SerializeField] DistractionLevelData distractionLevelData; // Reference to the distraction level data
    [SerializeField] Dropdown distLevelDropdown; // UI dropdown for selecting distraction levels

    // Initializes the distraction level data when the script is enabled
    private void OnEnable()
    {
        distractionLevelData.distractionLevel = DistractionLevel.None; // Set the default distraction level to "None"
        distractionLevelData.withAudio = false; // Disable audio distractions by default
    }

    // Updates the distraction level based on the dropdown value
    public void RegisterDistLevel()
    {
        // Assign the selected distraction level from the dropdown to the distraction level data
        distractionLevelData.distractionLevel = (DistractionLevel)(distLevelDropdown.value);
    }

    // Toggles the state of audio distractions
    public void ControlDistractionAudio()
    {
        // Invert the current audio setting (enable/disable audio distractions)
        distractionLevelData.withAudio = !distractionLevelData.withAudio;
    }
}