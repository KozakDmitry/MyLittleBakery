using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Sprite[] volumeSprites,languageSprites;
    [SerializeField] AudioSource audioMenu;
    [SerializeField] private GameObject firstUI,playUI,settingsUI;
    [SerializeField] private Button continieGame;
    [SerializeField] private CanvasGroup loadingCanvasGroup;
    [SerializeField] private RectTransform langContainer;
    private float expandedHeight = 200f;
    private bool isExpanded = false;
    private void StartTransition()
    {
        loadingCanvasGroup.interactable = true;
        loadingCanvasGroup.DOFade(1, 1.0f).OnComplete(() =>
        {
            AsyncOperation asyncLoad = SaveLoadHelp.LoadSceneAsync("GameScene");
            asyncLoad.completed += OnLoadCompleted;
        });
       


     
    }

    public void ToggleDropdown()
    {
        if (isExpanded)
        {

            langContainer.DOSizeDelta(new Vector2(langContainer.sizeDelta.x, 0f), 0.3f);
        }
        else
        {

            langContainer.DOSizeDelta(new Vector2(langContainer.sizeDelta.x, expandedHeight), 0.3f);
        }

        isExpanded = !isExpanded;
    }



    private void OnLoadCompleted(AsyncOperation asyncOperation)
    {
        if (SaveLoadHelp.continieGame)
        {
            SaveLoadHelp.LoadAllData();
        }
        SceneManager.LoadScene("GameScene");
    }
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
        StartTransition();
     
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
