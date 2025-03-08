using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Quits the application when called
    public void Exit()
    {
        Application.Quit(); // This will close the application. Note: This won't work in the Unity Editor.
    }

    // Restarts the game by loading the first scene (index 0 in the build settings)
    public void RestartGame()
    {
        SceneManager.LoadScene(0); // Load the first scene in the build index
    }

    // Loads a specific scene by its name
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName); // Load the scene with the specified name
    }
}