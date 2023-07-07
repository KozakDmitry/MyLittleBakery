using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class SafeManager : MonoBehaviour, ISaveable
{

    [SerializeField]
    private int countOfElements, countOfGold, countOfEx;

    private enum safeTrigger { Open, Close, OpenedSafe };
    private void Awake()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
    }
    private void Start()
    {
    }
   
    public void Save()
    {
        JSONObject save = new JSONObject();
        save.Add("countOfElements", countOfElements);
        save.Add("countOfGold", countOfGold);
        save.Add("countOfEx", countOfEx);
        SaveLoadHelp.saveFile.Add("Safe", save);
    }

    private void OnDestroy()
    {
        SaveLoadHelp.SaveAll -= Save;
        SaveLoadHelp.LoadAll -= Load;
    }
    public void Load()
    {
        JSONObject saveData = new JSONObject();
        saveData.Add(SaveLoadHelp.saveFile["Safe"]);
        countOfElements = saveData["codeValue"];
        countOfGold = saveData["countOfGold"];
        countOfEx = saveData["countOfEx"];

    }


    
}