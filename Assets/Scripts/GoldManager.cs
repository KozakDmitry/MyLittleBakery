using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour, ISaveable
{

    [SerializeField] private Text CurrentGoldText;
    [SerializeField] private Text ExperienceText;
    private float currentGold = 0;
    private float currentExperience = 0;
    [SerializeField] private LevelManager levelManager;


    private static GoldManager instance = null;


    private GoldManager()
    {

    }

    public static GoldManager GetInstance()
    {
        return instance;
    }
    public void Save()
    {

    }
    public void Load()
    {

    }
    public void UpdateCurrentGold(float goldToAdd)
    {
        currentGold += goldToAdd;
        CurrentGoldText.text = currentGold.ToString();

    }
    public float GetGold()
    {
        return currentGold;
    }
    public void UpdateCurrentExperience(float expToAdd)
    {

        currentExperience += expToAdd;
        ExperienceText.text = currentExperience.ToString();
    }

    // Start is called before the first frame update
    void Awake()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        
    }

    
    // Update is called once per frame
    void Update()
    {

    }
}
