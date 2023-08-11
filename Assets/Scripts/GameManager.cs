using System.Collections;
using System.Collections.Generic;
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
