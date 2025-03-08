using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSelectionUIManager : MonoBehaviour
{
    [SerializeField] GameData gameData; // Reference to the GameData ScriptableObject to store max time and correct hits
    [SerializeField] Dropdown timeDropdown; // Dropdown menu for selecting the time settings

    // Initializes default values for max time and correct hits when the script is enabled
    private void OnEnable()
    {
        gameData.MaxNumberOfCorrectHits = 4; // Set the default maximum number of correct hits
        gameData.SetMaxTime(20); // Set the default maximum game time to 20 seconds
    }

    // Updates the game data based on the selected dropdown value
    public void RegistergameDataDropdownValue()
    {
        // Parse the selected dropdown value to set the max number of correct hits
        gameData.MaxNumberOfCorrectHits = int.Parse(timeDropdown.options[timeDropdown.value].text) / 5;

        // Parse the selected dropdown value to set the maximum game time
        gameData.SetMaxTime(float.Parse(timeDropdown.options[timeDropdown.value].text));
    }
}