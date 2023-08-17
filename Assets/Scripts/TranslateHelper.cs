using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateHelper : MonoBehaviour
{
    [SerializeField]
    private TranslateObj[] allTranslateObj;


    private void Start()
    {
        TranslateManager.instance.AddObjects(allTranslateObj);
    }
}
