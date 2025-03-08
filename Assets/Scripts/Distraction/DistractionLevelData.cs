using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a ScriptableObject to manage data related to distraction levels
[CreateAssetMenu(fileName = "DistractionLevelData", menuName = "DistractionLevelData/Create Distraction Level")]
public class DistractionLevelData : ScriptableObject
{
    public DistractionLevel distractionLevel; // Enum to specify the current distraction level
    public bool withAudio; // Flag indicating if the distraction includes audio
    public bool Adaptive; // Flag indicating if the distraction is adaptive
    public int csvIndex; // An index used for CSV logging or tracking data

    // Sets the distraction level using an integer value
    public void SetDistractionLevel(int _distractionLevel)
    {
        distractionLevel = (DistractionLevel)_distractionLevel; // Cast the integer to the DistractionLevel enum
    }

    // Sets whether the distraction is adaptive
    public void SetAdaptive(bool _b)
    {
        Adaptive = _b; // Update the Adaptive flag
    }

    // Calculates and sets the CSV index based on distraction level and whether it's adaptive
    public void SetCSVIndex(int _i)
    {
        if (distractionLevel == DistractionLevel.None)
        {
            csvIndex = _i + 0; // No additional offset if there is no distraction level
        }
        else if (!Adaptive && distractionLevel != DistractionLevel.None)
        {
            csvIndex = _i + 3; // Apply an offset of 3 for non-adaptive distractions
        }
        else if (Adaptive)
        {
            csvIndex = _i + 6; // Apply an offset of 6 for adaptive distractions
        }
    }
}