using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;


public static class Translator
{
    private static string fileName = "Languages.csv";
    private static string path = Application.dataPath + "/Localization/" + fileName;
    private static string choosedLanguage;
    private static string phrase;
    private static string[] phrases;
    private enum Languages
    {
        Russian,
        English
    }

    public static string SelectStartLanguage()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            choosedLanguage = LoadLanguage();
        }
        return choosedLanguage;
    }
    private static int ReturnLanguage()
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
    }
    public static void ChangeLanguage(int choose)
    {

    }

    public static void ReadCSVFile()
    {
        phrases = File.ReadAllLines(path, Encoding.UTF8);
    }
}
