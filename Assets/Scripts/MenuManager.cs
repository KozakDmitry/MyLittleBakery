using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        if (SaveLoadHelp.continieGame)
            SaveLoadHelp.LoadAllData();
    }
    public void SaveGame()
    {
        SaveLoadHelp.SaveAllData();
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
