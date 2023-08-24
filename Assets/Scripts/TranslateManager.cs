using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TranslateManager : MonoBehaviour
{
    private TranslateObj[] allTranslateObj;
    public static TranslateManager instance;
    [SerializeField] private GameObject languagePrefab;
    [SerializeField] private RectTransform langContainer;
    [SerializeField] private Sprite[] languageSprites;
    private float expandedHeight = 150f;
    private bool isExpanded = false;
    private List<GameObject> lanBlockList = new List<GameObject>();




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
        TranslateHelper helper = (TranslateHelper)FindObjectOfType(typeof(TranslateHelper));
        allTranslateObj = helper.GetPhrases();
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
            Debug.Log(i);
            GameObject langBlock =  Instantiate(languagePrefab, langContainer);
            Image picture = langBlock.GetComponent<Image>();
            picture.sprite = languageSprites[i];
            picture.raycastTarget = false;
            Button myButton = langBlock.GetComponent<Button>();
            myButton.onClick.AddListener(() => ChangeLanguage(Translator.ReturnStringLanguage(i), langBlock));
            lanBlockList.Add(langBlock);
            myButton.interactable = false;
        }
    }
    public void ToggleDropdown()
    {
        if (isExpanded)
        {
            foreach (GameObject gm in lanBlockList)
            {
                gm.GetComponent<Image>().raycastTarget = false;              
                gm.GetComponent<Button>().interactable = false;
            }
           
            langContainer.DOSizeDelta(new Vector2(langContainer.sizeDelta.x, langContainer.sizeDelta.y - expandedHeight), 0.3f);
            langContainer.DOMoveY(langContainer.position.y + 0.5f, 0.3f);
        }
        else
        {
            foreach (GameObject gm in lanBlockList)
            {
                gm.GetComponent<Image>().raycastTarget = true;
                gm.GetComponent<Button>().interactable = true;
            }
            langContainer.DOMoveY(langContainer.position.y - 0.5f, 0.3f);
            langContainer.DOSizeDelta(new Vector2(langContainer.sizeDelta.x, langContainer.sizeDelta.y + expandedHeight), 0.3f);


        }

        isExpanded = !isExpanded;
    }

    public void ChangeLanguage(string language,GameObject gm)
    {
        Debug.Log(language);
        Translator.ChangeLanguage(language);
        gm.transform.SetAsFirstSibling();
        ChangeLan();
        ToggleDropdown();
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
