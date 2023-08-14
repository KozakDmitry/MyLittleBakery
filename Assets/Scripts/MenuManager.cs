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
    [SerializeField] private GameObject firstUI,playUI,settingsUI;
    [SerializeField] private Button continieGame;



    private void Start()
    {
        if (SaveLoadHelp.CheckSave())
        {
            continieGame.interactable = true;
        }
        else
        {
            continieGame.interactable = false;
        }
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
        print(SaveLoadHelp.continieGame);
        SceneManager.LoadScene("GameScene");
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


    
    public void Exit()
    {
        Application.Quit();
    }

  
    
   
}
