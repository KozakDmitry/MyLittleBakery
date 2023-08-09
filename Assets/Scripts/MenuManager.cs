using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Sprite[] volumeSprites;
    [SerializeField] AudioSource audioMenu;
    [SerializeField] private GameObject firstUI,playUI,settingsUI, ExitUI;

    private static GameObject instance;

    private void Start()
    {
        if (SaveLoadHelp.continieGame)
            SaveLoadHelp.LoadAllData();
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }
    public void SaveGame()
    {
        SaveLoadHelp.SaveAllData();
    }

    public void Play(int k)
    {
        switch (k)
        {
            case 0:
                SaveLoadHelp.continieGame = false;
                break;
            case 1:
                SaveLoadHelp.continieGame = true;
                break;
            default:
                SaveLoadHelp.continieGame = true;
                break;
        }
        SceneManager.LoadScene("MainScene");
    }
    public void ChangeToPlayUI()
    {
        firstUI.SetActive(false);
        playUI.SetActive(true);
    }
    
    public void BackToMain(GameObject gm)
    {
        gm.SetActive(false);
        firstUI.SetActive(true);
    }
    public void Settings()
    {
        firstUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void OpenElement(GameObject element)
    {
        element.SetActive(!element.activeSelf);
    }

    public void SwitchVolume(Image image)
    {
        if (audioMenu.mute)
        {
            image.sprite = volumeSprites[0];
        }
        else
        {
            image.sprite = volumeSprites[1];
        }
        audioMenu.mute = !audioMenu.mute;
        
    }

    public void ToExitMenu(GameObject gm)
    {
        gm.SetActive(false);
        ExitUI.SetActive(true);
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
   
}
