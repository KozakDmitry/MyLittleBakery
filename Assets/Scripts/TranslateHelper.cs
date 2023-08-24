using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateHelper : MonoBehaviour
{
    [SerializeField]
    private TranslateObj[] allTranslateObj;


    public TranslateObj[] GetPhrases()
    {
        return allTranslateObj;
    }
}
