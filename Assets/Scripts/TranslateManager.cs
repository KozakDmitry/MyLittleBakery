using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TranslateManager : MonoBehaviour
{
    private TranslateObj[] allTranslateObj;
    public static TranslateManager instance;
    [SerializeField] private GameObject languagePrefab;
    [SerializeField] private RectTransform langContainer;
    [SerializeField] private Sprite[] languageSprites;
    private float expandedHeight;
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
       
        Translator.SelectStartLanguage();
        Translator.ReadCSVFile();

        GenerateFirstLanguages();
    }

    public void OnEnable()
    {
        UpdateLangBlock();
    }

    public void GenerateFirstLanguages() 
    {
        expandedHeight = 0;
        for (int i = 0; i < Translator.GetLanguagesCount(); i++)
        {
            int index = i;
            GameObject langBlock =  Instantiate(languagePrefab, langContainer);
            expandedHeight += langBlock.GetComponent<RectTransform>().rect.height-10;
            Image picture = langBlock.GetComponent<Image>();
            picture.sprite = languageSprites[i];
            picture.raycastTarget = false;
            Button myButton = langBlock.GetComponent<Button>();
            myButton.onClick.AddListener(() => ChangeLanguage(Translator.ReturnStringLanguage(index), langBlock));
            lanBlockList.Add(langBlock);
            myButton.interactable = false;
        }
    }

    private void UpdateLangBlock()
    {
        Debug.Log("Hi");
        for(int i = 0;i < lanBlockList.Count; i++)
        {
            lanBlockList[i].transform.SetSiblingIndex(i);
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
        Translator.ChangeLanguage(language);
        gm.transform.SetAsFirstSibling();    
        lanBlockList.RemoveAt(lanBlockList.IndexOf(gm));
        lanBlockList.Insert(0, gm);
        Translator.UpdatePhrases();
        ToggleDropdown();
    }


    public void AddObjects(TranslateObj[] NewTranslateObj)
    {
        allTranslateObj = NewTranslateObj;
        Translator.UpdatePhrases();
    }
   
}
