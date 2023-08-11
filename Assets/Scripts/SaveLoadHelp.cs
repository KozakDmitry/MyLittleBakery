using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

public static class SaveLoadHelp
{
    public delegate void SaveContainer();
    public static event SaveContainer SaveAll = delegate { };
    public delegate void LoadContainer();
    public static event LoadContainer LoadAll = delegate { };
    private static string saveFileName = "/Saves.json";
    private static string pathFile = Application.persistentDataPath + saveFileName;
    public static JSONObject saveFile = new JSONObject();
    public static bool continieGame = false;
    public static void SubscribeSV(GameObject gm)
    {
        ISaveable isave = gm.GetComponent<ISaveable>();
        SaveAll += isave.Save;
        LoadAll += isave.Load;

    }
    public static bool CheckSave()
    {
        if (File.Exists(pathFile))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void SaveAllData()
    {
        SaveAll();
        File.WriteAllText(pathFile, saveFile.ToString());
    }

    public static void LoadAllData()
    {
        if (File.Exists(pathFile))
        {
            string JsonFile = File.ReadAllText(pathFile);
            saveFile = (JSONObject)JSON.Parse(JsonFile);
            LoadAll();
        }
        else
        {
            continieGame = false;
        }
        
    }
    public static void ResetAllProgress()
    {
        File.Delete(pathFile);
        PlayerPrefs.DeleteAll();
    }

}