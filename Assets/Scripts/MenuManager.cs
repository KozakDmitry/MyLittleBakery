using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Sprite[] volumeSprites,shopSprites;
    [SerializeField] AudioSource audioMenu;
    [SerializeField] private GameObject firstUI,playUI,settingsUI;
    [SerializeField] private Slider audioSlider;
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
    public void Play()
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

    public void OpenShop(Image image)
    {
        
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

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
   
}
