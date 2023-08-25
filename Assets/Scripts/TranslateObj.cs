using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TranslateObj : MonoBehaviour
{
    public int numText;
    private bool startedActive = false;

    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        startedActive = gameObject.activeSelf;
    }
    private void OnEnable()
    {
        if (!startedActive)
        {         
            ChangeText(Translator.SendPhrase(numText));
        }
        else
        {
            startedActive = true;
        }
       
    }
    public void ChangeText(string textMessage)
    {
        try
        {
            if (gameObject.activeInHierarchy)
            {
                text.text = textMessage;
            }
        }
        catch
        {
            Debug.Log(gameObject.name + " ebana ");
        }

    }
}
