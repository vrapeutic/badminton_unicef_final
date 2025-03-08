using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager Singleton; // Singleton instance for global access

    private void Awake()
    {
        // Ensure there is only one active instance of AudioManager
        if (Singleton == null)
            Singleton = this;
        else
        {
            if (Singleton != this)
                Destroy(gameObject); // Destroy duplicate instances
        }
    }

    #endregion

    // Structure to hold audio clip data with its corresponding SFX type
    public struct AudioStruct
    {
        public AudioClip audioClip; // Audio clip file
        public SFX SFXEnum; // Enum to specify the SFX type
    }

    [Header("SFX")]
    [SerializeField] private AudioMixerGroup sfx; // Audio mixer for sound effects
    [SerializeField] private AudioSource sfxSource; // Audio source to play SFX
    [SerializeField] private List<AudioClip> sfxAudioClips; // List of individual SFX audio clips
    [SerializeField] private List<SFXAudioDataSO> sFXAudioDataSOs; // List of SFX audio data as ScriptableObjects
    private List<string> sFXAudioNames = new List<string>(); // List of SFX clip names for reference

    [Space(10)]
    [Header("Music")]
    [SerializeField] private bool muteMusic; // Flag to mute/unmute music
    [SerializeField] private AudioMixerGroup bgMusic; // Audio mixer for background music
    [SerializeField] private AudioSource musicSource; // Audio source for background music
    [SerializeField] private List<AudioClip> musicAudioClips; // List of background music audio clips

   private readonly float maxVolume = 0.5f; // Maximum volume level
    private bool isMusicOn = true; // Tracks whether music is currently playing
    private float currentMusicLength; // Length of the current music track
    private int currentMusicIndex = 0; // Index of the current music track
    private float startFadeOutAt_Percentage = 0.8f; // Percentage at which to start fading out music
    private float decreaseVolumeBy; // Rate of volume decrease for fading out music
    private float currentMusicTime; // Current playback time of the music track

    private void Start()
    {
        // Initialize music source volume and set up music playback
        musicSource.volume = 0.5f;
        currentMusicIndex = -1;

        // Mute music if specified
        if (muteMusic)
            MuteMusic(!muteMusic);

        // Populate list of SFX names from ScriptableObjects
        for (int i = 0; i < sFXAudioDataSOs.Count; i++)
        {
            sFXAudioNames.Add(sFXAudioDataSOs[i].SFXClipName);
        }
    }

    private void Update()
    {
        // Update current music time if music is playing
        if (!isMusicOn)
            return;

        currentMusicTime = musicSource.time;
    }

    // Plays an SFX based on its name from a ScriptableObject
    public void PlaySFXBasedOnSO(string audioClip)
    {
        if (!DistractionManager.Singleton.IsAdaptive()) return; // Skip if not in adaptive mode

        int audioClipIndex = sFXAudioNames.IndexOf(audioClip); // Find index of the audio clip by name

        // Play the audio clip and display subtitles based on the language setting
        if (LanguageManager.Instance.IsEnglish)
        {
            sfxSource.PlayOneShot(sFXAudioDataSOs[audioClipIndex].languageVoiceDatas[0].audioClip, 1);
            EventsManager.OnSendSubtitleUI?.Invoke(sFXAudioDataSOs[audioClipIndex].languageVoiceDatas[0].audioSubtitle);
        }
        else
        {
            sfxSource.PlayOneShot(sFXAudioDataSOs[audioClipIndex].languageVoiceDatas[1].audioClip, 1);
            EventsManager.OnSendSubtitleUI?.Invoke(sFXAudioDataSOs[audioClipIndex].languageVoiceDatas[1].audioSubtitle);
        }
    }

    // Plays an SFX from the list based on its index
    public void PlaySFX(int sfx)
    {
        sfxSource.PlayOneShot(sfxAudioClips[sfx], 1);
    }

    // Plays the next background music clip in the playlist
    public void RunNextMusicClip()
    {
        currentMusicIndex++;
        if (currentMusicIndex >= musicAudioClips.Count)
            currentMusicIndex = 0; // Loop back to the first track

        // Set and play the next music clip
        musicSource.clip = musicAudioClips[currentMusicIndex];
        currentMusicLength = musicAudioClips[currentMusicIndex].length;
        decreaseVolumeBy = maxVolume / ((1 - startFadeOutAt_Percentage) * currentMusicLength);
        musicSource.volume = 0.1f; // Start with low volume
        musicSource.Play();
    }

    // Toggles the mute state of the music
    public void MuteMusic(bool isOn)
    {
        isMusicOn = isOn;

        // Adjust music mixer volume based on mute state
        if (isOn)
        {
            bgMusic.audioMixer.SetFloat(bgMusic.name + "Volume", 0f);
        }
        else
        {
            bgMusic.audioMixer.SetFloat(bgMusic.name + "Volume", -80f);
        }
    }
}

// Enum for sound effects (SFX)
public enum SFX
{
    SFX_ButtonClick,
    SFX_BallHit,
    SFX_Birds,
    SFX_Hey,
    SFX_Correct,
    SFX_Slow
}
