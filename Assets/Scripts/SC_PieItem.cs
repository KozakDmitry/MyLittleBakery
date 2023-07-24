using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;

public class SC_PieItem : MonoBehaviour
{

    [SerializeField] Sprite[] pieImages;
    [SerializeField] int pieLevel;
    [SerializeField] GameObject TextToRender;
    private int pieIndex;
    private int numOfCell;
    private float goldIncome;

    public void SetCell(int cellIndex)  { numOfCell = cellIndex;  }
    public int GetCell() {  return numOfCell;}
    public void SetIndex(int index)  { numOfCell = index; }
    public int GetIndex() { return numOfCell; } 


 
    public int GetPieLevel() { return pieLevel; }

    public void UpdatePieLevel()
    {
        pieLevel++;
        GetComponent<SpriteRenderer>().sprite = pieImages[pieLevel];
        GoldManager.GetInstance().UpdateCurrentExperience(pieLevel * 2+1);
    }

    public void ClearPie()
    {
        pieLevel = -1;
        CancelInvoke(nameof(AddGoldIncome));
        TextMeshIsActive(false);
        GetComponent<SpriteRenderer>().sprite = null;
        //RemovePie(this.gameObject);
    }

    public void SpawnPie()
    {
        pieLevel = 0;
        GetComponent<SpriteRenderer>().sprite = pieImages[0];
        InvokeRepeating(nameof(AddGoldIncome), 1.0f, 3.0f);
        GoldManager.GetInstance().UpdateCurrentExperience(1.0f);
    }

    private void AddGoldIncome()
    {
        goldIncome = Mathf.Pow(2, pieLevel);
        TextMeshIsActive(true);
        TextToRender.GetComponent<TextMesh>().text = goldIncome.ToString();
        GoldManager.GetInstance().UpdateCurrentGold(goldIncome);
        Invoke(nameof(DiactivateTextMesh), 1.0f);
    }

    private void Start()
    {
        SetIndex(GetInstanceID());
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
