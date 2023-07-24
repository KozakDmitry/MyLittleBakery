using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{

    [SerializeField] private Text CurrentGoldText;
    [SerializeField] private Text ExperienceText;
    private float currentGold;
    private float currentExperience;
    [SerializeField] private LevelManager levelManager;
    
    
    
    public void UpdateCurrentGold(float goldToAdd)
    {
        currentGold += goldToAdd;
        CurrentGoldText.text = currentGold.ToString();
    }
    public void GetGold()
    {

    }
    public void UpdateCurrentExperience(float expToAdd)
    {
        currentExperience += expToAdd;
        ExperienceText.text = currentExperience.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        float.TryParse(CurrentGoldText.text, out currentGold);
        float.TryParse(ExperienceText.text, out currentExperience);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
