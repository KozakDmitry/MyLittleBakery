using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;
using Mono.Cecil.Cil;

public static class Translator
{
    private static string fileName = "Languages.csv";
    private static string path = Application.dataPath + "/Localization/" + fileName;
    private static string choosedLanguage;
    private static string[] phrases;
    private enum Languages
    {
        Russian = 0,
        English = 1,
    }

    public static string ReturnStringLanguage(int i) 
    {
        Debug.Log(i);
        if (Enum.GetName(typeof(Languages), i) != null) { return Enum.GetName(typeof(Languages), i); }

        else
        {
            return Languages.Russian.ToString();
        }
    }
    public static int GetLanguagesCount()
    {
        int count = Enum.GetNames(typeof(Languages)).Length;
        return count;
    }
   

    public static string SelectStartLanguage()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            choosedLanguage = LoadLanguage();
            SaveLanguage();
        }
        return choosedLanguage;
    }


    private static void SaveLanguage()
    {
        PlayerPrefs.SetString("Language", choosedLanguage);
        PlayerPrefs.Save();
    }
    public static int ReturnLanguage()
    {
        switch (choosedLanguage)
        {
            case "Russian":
                return 0;
            case "English":
                return 1;
            default:
                return 0;
        }
    }
    public static string SendPhrase(int num)
    {
        var data = phrases[num].Split(';');

        return data[ReturnLanguage()];
    }
    private static string LoadLanguage()
    {

        return Enum.IsDefined(typeof(Languages), PlayerPrefs.GetString("Language")) ? PlayerPrefs.GetString("Language") : Application.systemLanguage.ToString();
    }

    public static void ChangeLanguage(string choose)
    {
        choosedLanguage = choose;

        SaveLanguage();
    }


    public static void ReadCSVFile()
    {
  
        phrases = File.ReadAllLines(path, Encoding.UTF8);
    }
}
