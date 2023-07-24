using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour, ISaveable
{

    [SerializeField] private static Text CurrentGoldText;
    [SerializeField] private static Text ExperienceText;
    private static float currentGold;
    private static float currentExperience;
    [SerializeField] private LevelManager levelManager;


    public void Save()
    {

    }
    public void Load()
    {

    }
    public static void UpdateCurrentGold(float goldToAdd)
    {
        currentGold += goldToAdd;
        CurrentGoldText.text = currentGold.ToString();
    }
    public float GetGold()
    {
        return currentGold;
    }
    public static void UpdateCurrentExperience(float expToAdd)
    {
        currentExperience += expToAdd;
        ExperienceText.text = currentExperience.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
        float.TryParse(CurrentGoldText.text, out currentGold);
        float.TryParse(ExperienceText.text, out currentExperience);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
