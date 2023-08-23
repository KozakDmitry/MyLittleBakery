using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TranslateManager : MonoBehaviour
{
    private TranslateObj[] allTranslateObj;
    public static TranslateManager instance;
    [SerializeField] private GameObject languagePrefab;
    [SerializeField] private RectTransform langContainer;
    [SerializeField] private Sprite[] languageSprites;
    private float expandedHeight = 150f;
    private bool isExpanded = false;

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
        GenerateFirstLanguages();
    }

    public void GenerateFirstLanguages() 
    {
        for (int i = 0; i < Translator.GetLanguagesCount(); i++)
        {
            GameObject langBlock =  Instantiate(languagePrefab, langContainer);
            langBlock.GetComponentInChildren<Image>().sprite = languageSprites[i];
        }
    }
    public void ToggleDropdown()
    {
        if (isExpanded)
        {

            Sequence mySequence = DOTween.Sequence();
            mySequence
                .Append(langContainer.DOSizeDelta(new Vector2(langContainer.sizeDelta.x, langContainer.sizeDelta.y - expandedHeight), 0.3f))
                .Append(langContainer.DOMoveY(langContainer.position.y + 0.5f, 0.3f));
        }
        else
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence
                .Append(langContainer.DOMoveY(langContainer.position.y - 0.5f, 0.3f))
                .Append(langContainer.DOSizeDelta(new Vector2(langContainer.sizeDelta.x, langContainer.sizeDelta.y + expandedHeight), 0.3f));


        }

        isExpanded = !isExpanded;
    }

    private void RefreshLanguage()
    {
     
    }

    public void ChangeLanguage(string language)
    {
        Translator.ChangeLanguage(language);
        RefreshLanguage();
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
