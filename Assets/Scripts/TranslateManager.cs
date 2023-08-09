using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateManager : MonoBehaviour,ISaveable
{
    private TranslateObj[] allTranslateObj;



    private void Awake()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
    }
    void Start()
    {
        Translator.ReadCSVFile();
        allTranslateObj = FindObjectsOfType<TranslateObj>() as TranslateObj[];
        ChangeLan();
    }
    public void Save()
    {

    }
    public void Load()
    {

    }
    public void ChangeLan()
    {
        foreach (TranslateObj obj in allTranslateObj)
        {
            obj.ChangeText(Translator.SendPhrase(obj.numText));
        }
    }
}
