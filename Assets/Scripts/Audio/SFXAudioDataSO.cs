using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject for managing SFX (sound effects) data in multiple languages
[CreateAssetMenu(fileName = "New SFX", menuName = "Audio System/New SFX")]
public class SFXAudioDataSO : ScriptableObject
{
    public string SFXClipName; // Name of the sound effect (used for reference and identification)
    public List<LanguageVoiceData> languageVoiceDatas = new List<LanguageVoiceData>(); 
    // List of LanguageVoiceData objects to store localized audio clips and subtitles
}

[System.Serializable]
// Structure to hold subtitle and audio clip data for different languages
public struct LanguageVoiceData
{
    public string audioSubtitle; // Subtitle text associated with the audio clip
    public AudioClip audioClip; // Audio clip for the corresponding language
}