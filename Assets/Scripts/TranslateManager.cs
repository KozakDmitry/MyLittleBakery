using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateManager : MonoBehaviour
{
    [SerializeField]
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
        SaveLoadHelp.SubscribeSV(this.gameObject);
        Translator.SelectStartLanguage();
    }

    void Start()
    {
        Translator.ReadCSVFile();
        allTranslateObj = FindObjectsOfType<TranslateObj>() as TranslateObj[];
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
