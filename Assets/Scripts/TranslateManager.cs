using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class TranslateManager : MonoBehaviour
{
    private TranslateObj[] allTranslateObj;
    public static TranslateManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Translator.SelectStartLanguage();
    }

    void Start()
    {
        Translator.ReadCSVFile();
      
    }
  

    
    public void AddObjects(TranslateObj[] NewTranslateObj)
    {
        allTranslateObj = NewTranslateObj;
        ChangeLan();
    }
    public void ChangeLan()
    {
        foreach (TranslateObj obj in allTranslateObj)
        {
            obj.ChangeText(Translator.SendPhrase(obj.numText));
        }
    }
}
