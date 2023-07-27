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
    [SerializeField] Sprite[] volumeSprites;
    [SerializeField] AudioSource audioMenu;
    [SerializeField] private GameObject firsUI;
    [SerializeField] private GameObject playUI;
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
        firsUI.SetActive(false);
        playUI.SetActive(true);
    }
    
    public void BackToMain(GameObject gm)
    {
        gm.SetActive(false);
        firsUI.SetActive(true);
    }
    public void Settings()
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
