using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using UnityEngine.Android;

// A serializable structure to hold data related to session events
[Serializable]
public struct DataStrings
{
    public string TargetStartingTime; // Timestamp when the target starts
    public string TargetHittingTime; // Timestamp when the target is hit
    public string InterruptionDuration; // Duration of the interruption
    public string DistractorName; // Name of the distractor
    public string TimeFollowingIt; // Time following the distractor
}

public class CsvReadWrite : MonoBehaviour
{
    #region Singleton

    public static CsvReadWrite Instance;

    // Ensures only one instance of CsvReadWrite exists
    private void Awake()
    {
        if (Instance == null)
            Instance = this; // Assign the current instance
        else
        {
            if (Instance != this)
                Destroy(gameObject); // Destroy duplicate instances
        }
    }

    #endregion

    [SerializeField] DistractionLevelData distractionLevelData; // Data about distraction levels
    [SerializeField] List<DataStrings> _dataStrings; // List to hold session data
    [SerializeField] StringVariable fileNameFromID; // Variable for deriving filename from session ID
    [SerializeField] BridgePluginInitializer bridge; // Reference to a plugin bridge initializer

    string FileName = ""; // Name of the CSV file
    TextWriter tw; // Writer for appending text to files
    string collectedDataAsString = ""; // String to hold collected data
    private bool alreadyWrote; // Flag to ensure the file is written only once

    void Start()
    {
        alreadyWrote = false; // Initialize write flag
        EventsManager.OnWriteCSV += CreateFile; // Subscribe to the event for writing CSV
    }

    private void OnDestroy()
    {
        EventsManager.OnWriteCSV -= CreateFile; // Unsubscribe from the event
    }

    // Context menu option to manually trigger CSV file creation in the Unity Editor
    [ContextMenu("Load CSV")]
    public void TestManualSave()
    {
        CreateFile();
    }

    public void CreateFile()
    {
        if (alreadyWrote) return;

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }

        // Construct the file path for Android devices
        //FileName = CreateDirectory( GetDownloadFolder() + "/VRapeuticSessions/") + fileNameFromID.Value + ".csv";                          //Yubreevi the updated one

        FileName = GetDownloadFolder() + "/VRapeuticSessions/" + DateTime.Now + ".csv"; //standalone

        //FileName = Application.persistentDataPath + "/PlaySessionsCSVData.csv";                          //editor

        WriteCSV(); // Write data to the CSV file

        alreadyWrote = true; // Mark file as written
        //bridge.SendIntent(FileName);   ///verry imporatnt for desktop
    }

    // Gets the path to the Download folder on Android
    public string GetDownloadFolder()
    {
        string[] temp =
            (Application.persistentDataPath.Replace("Android", "")).Split(new string[] { "//" },
                System.StringSplitOptions.None);
        return (temp[0] + "/Download");
    }

    // Creates a directory if it doesn't exist
    public string CreateDirectory(string dir)
    {
        if (!Directory.Exists(dir))
        {
            var directory = Directory.CreateDirectory(dir); // Create the directory
        }

        return dir; // Return the directory path
    }

    // Writes a session header to the CSV file
    public void WriteSessionHeader()
    {
        File.AppendAllText(FileName, "Badminton" + ", " + (distractionLevelData.csvIndex + 1).ToString());
        File.AppendAllText(FileName, "\n");

        if (distractionLevelData.csvIndex < 7)
        {
            File.AppendAllText(FileName,
                "Target Starting Time" + ", " + "Target Hitting Time" + ", " + "Interruption Duration" + ", " +
                "Distractor Name" + ", " + "Time Following It" + "\n");
        }
    }

    // Writes non-adaptive session data to the internal list
    public void WriteNonAdaptiveDataRaw(string startStamp, string hitStamp, string InterruptionDuration)
    {
        Debug.Log("Data No Interruption");
        DataStrings dS = new DataStrings();
        dS.TargetStartingTime = startStamp + ", ";
        dS.TargetHittingTime = hitStamp + ", ";
        dS.InterruptionDuration = InterruptionDuration;

        _dataStrings.Add(dS); // Add the data to the list
    }

    // Adds distraction data to the internal list
    public void WriteDistarctionData(string distractorName, string timeFollowingIt)
    {
        DataStrings dS = new DataStrings();
        dS.TargetStartingTime = _dataStrings[_dataStrings.Count - 1].TargetStartingTime;
        dS.TargetHittingTime = _dataStrings[_dataStrings.Count - 1].TargetHittingTime;
        dS.InterruptionDuration = _dataStrings[_dataStrings.Count - 1].InterruptionDuration + ", ";
        dS.DistractorName = distractorName + ", ";
        dS.TimeFollowingIt = timeFollowingIt;

        _dataStrings.RemoveAt(_dataStrings.Count - 1); // Remove the old entry
        _dataStrings.Add(dS); // Add the updated data
    }

    // Context menu option to manually save data to CSV in the Unity Editor
    [ContextMenu("SaveFile")]
    public void WriteCSV()
    {
        WriteSessionHeader(); // Write the session header

        for (int i = 0; i < _dataStrings.Count; i++)
        {
            // Append each data entry to the collected data string
            collectedDataAsString += _dataStrings[i].TargetStartingTime + _dataStrings[i].TargetHittingTime +
                                     _dataStrings[i].InterruptionDuration +
                                     _dataStrings[i].DistractorName + _dataStrings[i].TimeFollowingIt;
            collectedDataAsString += "\n";
        }

        File.AppendAllText(FileName, collectedDataAsString); // Write the data to the file
        File.AppendAllText(FileName, "\n");
    }

    // Appends head position data to the CSV file
    public void WriteActualCSVHeadData(Vector3 headData)
    {
        tw.Close(); // Close any existing writer
        tw = new StreamWriter(FileName, true); // Open the file for appending

        tw.WriteLine(" " + ", " + headData.x + ", " + headData.y + ", " + headData.z);
        tw.Close(); // Close the writer
    }
}