using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ExitUI;


    private void Start()
    {
        if (SaveLoadHelp.continieGame)
            SaveLoadHelp.LoadAllData();
    }

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            SaveLoadHelp.SaveAllData();
        }
    }

    public void SaveGame()
    {
        SaveLoadHelp.SaveAllData();
    }

    public void LoadGame()
    {
        SaveLoadHelp.continieGame = true;
        SceneManager.LoadScene("GameScene");
    }
    public void ToExitMenu(GameObject gm)
    {
        gm.SetActive(false);
        ExitUI.SetActive(true);
    }
    public void OpenElement(GameObject element)
    {
        element.SetActive(!element.activeSelf);
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Exit()
    {
        Application.Quit();
    }


}
