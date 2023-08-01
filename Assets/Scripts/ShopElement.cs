using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    private string nameOfElement;
    private int levelOfPie;
    private int cost;
    [SerializeField] private GameObject blockElement;

    public GameObject getBlockElement() { return blockElement; }
    public void SetCost(int cost) { this.cost = cost; }
    public int GetCost() { return cost; }
    public void SetLevelOfPie(int level)  {  levelOfPie = level;  }
    public int GetLevelOfPie()  {  return levelOfPie;   }
    
    public void SetName(string name)  {nameOfElement = name; }
    public string GetName() { return name; }
    void Start()
    {
        
    }
    private void Update()
    {
        
    }
}
