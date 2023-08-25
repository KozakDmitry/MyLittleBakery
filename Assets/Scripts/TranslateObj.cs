using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TranslateObj : MonoBehaviour
{
    public int numText;

    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

    }
    private void OnEnable()
    {
        try
        {
            ChangeText(Translator.SendPhrase(numText));
        }
        catch
        {
            Debug.Log(gameObject.name + " ebana ");
        }
       
    }
    public void ChangeText(string textMessage)
    {
        try
        {
            text.text = textMessage;
        }
        catch 
        {
            Debug.Log(gameObject.name + ": " + textMessage);
        }
       
    }
}
