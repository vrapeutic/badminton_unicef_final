//The project is licensed under GPL-3.0, which requires all modifications and distributions to adhere to the same license.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCloseController : MonoBehaviour
{
    [SerializeField] GameEvent OnEndGame; // Reference to a GameEvent that is triggered when the game ends

    // Method to handle the end of the game
    public void EndGame()
    {
        OnEndGame.Raise(); // Trigger the OnEndGame event
        StartCoroutine(EndGameByCoroutine()); // Start the coroutine to delay closing the application
    }

    // Coroutine that waits for 5 seconds before quitting the application
    IEnumerator EndGameByCoroutine()
    {
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        string scenePath = SceneUtility.GetScenePathByBuildIndex(0);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        if (sceneName == "SystemLobby")
        {
            AndroidIntentHelper.LaunchExternalActivity();
            Application.Quit(); // Quit the application. Note: This will not work in the Unity Editor.
        }
        else
        {
            Application.Quit(); // Quit the application. Note: This will not work in the Unity Editor.
        }
    }
}