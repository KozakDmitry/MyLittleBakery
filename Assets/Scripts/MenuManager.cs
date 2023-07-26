using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        if (SaveLoadHelp.continieGame)
            SaveLoadHelp.LoadAllData();
        DontDestroyOnLoad(this.gameObject);
    }
    public void SaveGame()
    {
        SaveLoadHelp.SaveAllData();
    }

    public void Settings()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
   
}
