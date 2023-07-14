using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;

public class SC_PieItem : MonoBehaviour
{

    [SerializeField] Sprite[] pieImages;
    [SerializeField] int pieLevel;
    [SerializeField] GameObject TextToRender;

    private GameObject LevelManager;
    private float goldIncome;

<<<<<<< Updated upstream
=======
    public void setNum(int num)
    {
        numOfPie=num;
    }

    public int getNum() 
    {
        return numOfPie;
    }

    public void Save() 
    {

    }
    public void Load() 
    {
        
    }

>>>>>>> Stashed changes
    public void SetLevelManager(GameObject levelManager)
    {
        LevelManager = levelManager;
    }

    public int GetPieLevel() { return pieLevel; }

    public void UpdatePieLevel()
    {
        pieLevel++;
        GetComponent<SpriteRenderer>().sprite = pieImages[pieLevel];
        LevelManager.GetComponent<SC_LevelManager>().UpdateCurrentExperience(pieLevel*2);
    }

    public void ClearPie()
    {
        pieLevel = -1;
        CancelInvoke(nameof(AddGoldIncome));
        TextMeshIsActive(false);
        GetComponent<SpriteRenderer>().sprite = null;
        
    }

    public void SpawnPie()
    {
        pieLevel = 0;
        GetComponent<SpriteRenderer>().sprite = pieImages[0];
        InvokeRepeating(nameof(AddGoldIncome), 1.0f, 3.0f);
        LevelManager.GetComponent<SC_LevelManager>().UpdateCurrentExperience(1.0f);
    }

    private void AddGoldIncome()
    {
        goldIncome = Mathf.Pow(2, pieLevel);
        TextMeshIsActive(true);
        TextToRender.GetComponent<TextMesh>().text = goldIncome.ToString();
        LevelManager.GetComponent<SC_LevelManager>().UpdateCurrentGold(goldIncome);
        Invoke(nameof(DiactivateTextMesh), 1.0f);
    }

    private void Start()
    {
        if (pieLevel >= 0)
            GetComponent<SpriteRenderer>().sprite = pieImages[pieLevel];
    }

    private void TextMeshIsActive(bool status)
    {
        TextToRender.GetComponent<MeshRenderer>().enabled = status;
    }

    private void DiactivateTextMesh()
    {
        TextMeshIsActive(false);
    }
}
