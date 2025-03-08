using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a ScriptableObject to manage game-wide settings
[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings/Create GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private bool isEnglish; // Boolean to track whether the current language is English

    // Public property to access the isEnglish flag
    public bool IsEnglish => isEnglish;

    // Method to set the language settings based on an integer input
    public void SetLanguageSettings(int language)
    {
        if (language == 0) // If the value is 0, set language to non-English (e.g., Vietnamese)
        {
            isEnglish = false;
        }
        else if (language == 1) // If the value is 1, set language to English
        {
            isEnglish = true;
        }
    }
}