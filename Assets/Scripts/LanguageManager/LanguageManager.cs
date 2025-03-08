using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance; // Singleton instance for global access

    private void Awake()
    {
        // Initialize the Singleton instance
        if (Instance == null)
            Instance = this; // Assign the current instance
        else
            Destroy(gameObject); // Destroy duplicate instances

        // Make the LanguageManager persist across scenes
        DontDestroyOnLoad(gameObject);

        // If running on a desktop platform, use the language setting from the GameSettings ScriptableObject
        if (isDesktop)
            isEnglish = gameSettings.IsEnglish;

        Debug.Log("Awake"); // Log for debugging purposes
    }

    [Header("App Related settings")]
    [SerializeField] private bool isDesktop; // Flag to determine if the app runs on a desktop platform
    [SerializeField] private GameSettings gameSettings; // Reference to the GameSettings ScriptableObject

    [SerializeField] private bool isEnglish; // Indicates whether the current language is English

    // Public property to get the current language setting
    public bool IsEnglish => isEnglish;

    // Method to set the language based on a string identifier
    public void SetLanguage(string language)
    {
        if (language == "en") // If the language code is "en", set to English
            isEnglish = true;
        else // Otherwise, set to non-English
            isEnglish = false;
    }

    // Method to update the language settings in the LocalizationManager
    public void UpdateLanguage()
    {
        LocaliztionManager.Instance.UpdateLanguage(); // Call the method to update language in the LocalizationManager
    }
}