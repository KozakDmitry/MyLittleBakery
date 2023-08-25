using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TranslateObj : MonoBehaviour,ITranslate
{
    public int numText;

    TextMeshProUGUI text;
    private void Awake()
    {
        Translator.UpdateLanguage += ChangeText;
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {      
        ChangeText();
    }
    private void OnDestroy()
    {
        Translator.UpdateLanguage -= ChangeText;
    }
    public void ChangeText()
    {

        try
        {
            if (gameObject.activeInHierarchy)
            {
                text.text = Translator.SendPhrase(numText);
            }
        }
        catch
        {
            Debug.Log(gameObject.name);
        }
    }
}
