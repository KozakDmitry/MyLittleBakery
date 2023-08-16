using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ExitUI;
    [SerializeField] private CanvasGroup loadingCanvasGroup;

    private void Start()
    {
        if (SaveLoadHelp.continieGame)
        {
            SaveLoadHelp.LoadAllData();
        }
        LoadingComplete();
    }

  
    private void LoadingComplete()
    {
        loadingCanvasGroup.DOFade(0, 1.0f);
        loadingCanvasGroup.interactable = false;
        loadingCanvasGroup.blocksRaycasts = false;
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


    private void OnLoadCompleted(AsyncOperation asyncOperation)
    {
        if (SaveLoadHelp.continieGame)
        {
            SaveLoadHelp.LoadAllData();
        }
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGame()
    {
        SaveLoadHelp.continieGame = true;
        loadingCanvasGroup.interactable = true;
        loadingCanvasGroup.blocksRaycasts = true;
        
        loadingCanvasGroup.DOFade(1, 1.0f).OnComplete(() =>
        {
            AsyncOperation asyncLoad = SaveLoadHelp.LoadSceneAsync("GameScene");
            asyncLoad.completed += OnLoadCompleted;
        });
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
