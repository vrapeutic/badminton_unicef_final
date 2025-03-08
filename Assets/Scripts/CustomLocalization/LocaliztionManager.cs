using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocaliztionManager : MonoBehaviour
{
    // Singleton instance for global access
    public static LocaliztionManager Instance;

    private void Awake()
    {
        // Ensure there is only one instance of LocalizationManager
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Destroy duplicate instances
    }

    [SerializeField] private List<LocalizedTextData> localizedTextDataList = new List<LocalizedTextData>();
    // List of localized text data that links UI Text components with their localized strings

    private void Start()
    {
        UpdateLanguage(); // Initialize the text elements based on the current language setting
    }

    // Updates the language and refreshes all localized text elements
    public void UpdateLanguage()
    {
        int languageIndex = 0; // Default language index (e.g., English)

        // Check the LanguageManager for the current language setting
        if (LanguageManager.Instance.IsEnglish)
        {
            languageIndex = 0; // Index for English
        }
        else
        {
            languageIndex = 1; // Index for another language (e.g., non-English)
        }

        UpdateTexts(languageIndex); // Apply the language index to update text elements
    }

    // Loops through all localized text data and updates the UI elements
    private void UpdateTexts(int languageIndex)
    {
        for (int i = 0; i < localizedTextDataList.Count; i++)
        {
            // Set the text component to the localized string based on the language index
            localizedTextDataList[i].targetText.text = localizedTextDataList[i].localizedStrings[languageIndex];
        }
    }
}

// Serializable structure to link a TextMeshProUGUI element with its localized strings
[System.Serializable]
public struct LocalizedTextData
{
    public TextMeshProUGUI targetText; // The UI text component to update
    public List<string> localizedStrings; // The list of localized strings, where each index represents a language
}