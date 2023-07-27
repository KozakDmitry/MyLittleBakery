using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslateObj : MonoBehaviour
{
    public int numText;

    Text text;
    private void Start()
    {
        text = GetComponent<Text>();

    }
    public void ChangeText(string textMessage)
    {
        text.text = textMessage;
    }
}
