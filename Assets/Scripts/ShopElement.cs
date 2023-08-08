using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    private string nameOfElement;
    private int levelOfPie;
    private int cost;
    [SerializeField] private GameObject blockElement;
    [SerializeField] private Image imageOfPie;
    [SerializeField] private TextMeshProUGUI costText, nameText;
    public GameObject getBlockElement() { return blockElement; }
    public enum Available
    {
        NoMoney = 0,
        NoAccess =1,
        Yes = 2
    }
    public void SetCost(int cost) 
    { 
        this.cost = cost;
        costText.text = cost.ToString();
    }

    public void SetAvailable(Available available)
    {

        switch (available)
        {
            case Available.NoMoney:
                blockElement.SetActive(false);
                GetComponent<Button>().interactable = false;
                break;
            case Available.NoAccess:
                blockElement.SetActive(true);
                GetComponent<Button>().interactable = false;
                break;
            case Available.Yes:
                blockElement.SetActive(false);
                GetComponent<Button>().interactable = true;
                break;
            default:
                break;
        }
        
       

        
    }
    public int GetCost() { return cost; }
    public void SetLevelOfPie(int level)  {  levelOfPie = level;  }
    public int GetLevelOfPie()  {  return levelOfPie;   }
    
    public void SetSprite(Sprite image)
    {
        imageOfPie.sprite = image;
    }

    public void SetName(string name)  
    {
        nameOfElement = name;
        nameText.text = name;
    }
    public string GetName() { return nameOfElement; }
 
}
