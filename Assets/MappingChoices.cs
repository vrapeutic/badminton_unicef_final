using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MappingChoices : MonoBehaviour
{
    [SerializeField] StringVariable typeOfAttention;//sustained //selective //adaptive
    //choose environment
    [SerializeField] IntVariable sustainedValue;
    [SerializeField] IntVariable noOfDistractor;
    [SerializeField] DistractionLevelData distractionLevelData;
    [SerializeField] GameData gameData;
    [SerializeField] GameSettings gameSettings;

    //This function takes the data array fram desktop app and apply it the scriptable Object that holds the game settings
    
    public void Mapper(int[] settings)
    {
        /*if (settings[0] == 1) typeOfAttention.Value= "sustained";
        else if (settings[0] == 2) typeOfAttention.Value = "selective";
        else typeOfAttention.Value = "adaptive";

        if (settings[2] == 1) sustainedValue.Value = 20;
        else if (settings[2] == 2) sustainedValue.Value = 40;
        else sustainedValue.Value = 60;

        if (settings[3] == 1) noOfDistractor.Value = 1;
        else if (settings[3] == 2) noOfDistractor.Value = 2;
        else noOfDistractor.Value = 3; */

        if (settings[0] == 1)
        {
            distractionLevelData.SetDistractionLevel(0);
            distractionLevelData.SetAdaptive(false);
        }
        else if (settings[0] == 2)
        {
            distractionLevelData.SetAdaptive(false);
        }
        else distractionLevelData.SetAdaptive(true);

        if (settings[2] == 1)
        {
            gameData.SetMaxTime(20);
            gameData.MaxNumberOfCorrectHits = 4;
            distractionLevelData.SetCSVIndex(1);
        }
        else if (settings[2] == 2)
        {
            {
                gameData.SetMaxTime(40);
                gameData.MaxNumberOfCorrectHits = 8;
                distractionLevelData.SetCSVIndex(2);
            }
        }
        else if (settings[2] == 3)
        {
            {
                gameData.SetMaxTime(60);
                gameData.MaxNumberOfCorrectHits = 12;
                distractionLevelData.SetCSVIndex(3);
            }
        }

        if (settings[3] == 1)
        {
            distractionLevelData.SetDistractionLevel(1);
            distractionLevelData.SetCSVIndex(1);
        }
        else if (settings[3] == 2)
        {
            distractionLevelData.SetDistractionLevel(2);
            distractionLevelData.SetCSVIndex(2);
        }
        else if (settings[3] == 3)
        {
            distractionLevelData.SetDistractionLevel(3);
            distractionLevelData.SetCSVIndex(3);
        }
        else
        {
            distractionLevelData.SetDistractionLevel(0);
            distractionLevelData.SetCSVIndex(0);
        }

        if (settings[4] == 0) 
        {
            gameSettings.SetLanguageSettings(0);
        }
        else if (settings[4] == 1) //VTN
        {
            gameSettings.SetLanguageSettings(0);
        }
        else if (settings[4] == 2) //EN
        {
            gameSettings.SetLanguageSettings(1);
        }

        StartCoroutine(LoadGameScene());
    }

    public IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
