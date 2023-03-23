using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;

public class SC_PieItem : MonoBehaviour
{

    [SerializeField] Sprite[] pieImages;
    [SerializeField] int pieLevel;

    public int GetPieLevel() { return pieLevel; }

    void Start()
    {
        if(pieLevel >= 0)
            GetComponent<SpriteRenderer>().sprite = pieImages[pieLevel];
    }

    public void UpdatePieLevel()
    {
        pieLevel++;
        GetComponent<SpriteRenderer>().sprite = pieImages[pieLevel];

    }

    public void ClearPie()
    {
        pieLevel = -1;
        GetComponent<SpriteRenderer>().sprite = null;
    }
}
