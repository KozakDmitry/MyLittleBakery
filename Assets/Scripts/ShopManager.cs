using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour,ISaveable
{
    [SerializeField] private ScriptableShop[] pies;
    [SerializeField] private GameObject shopElement;
    [SerializeField] private GameObject shopCreator;
    [SerializeField] private LevelManager levelManager;
    private int maxValue=0, maxShop, lastChange=-1;
    private List<GameObject> shopPies = new List<GameObject>();

    private void Awake()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
        GoldManager.GoldValueChanged += OnGoldValueChanged;
        RefreshShop();
    }
    private void OnGoldValueChanged(float newValue)
    {
        maxValue = 0;
        for(int i = 0; i < pies.Length; i++)
        {
            if (newValue >= pies[maxValue].cost && maxValue != pies.Length)
            {
                maxValue++;
            }
            else break;
        }
        RefreshShop();
    }
    //private void OnGoldValueChanged(float newValue)
    //{
    //    onDecreace = false;
    //    onIncreace = false;
    //    do
    //    {
    //        if (newValue >= pies[maxValue].cost && !onDecreace && maxValue<pies.Count())
    //        {
    //            maxValue++;
    //            onIncreace = true;
    //        }
    //        else if (newValue < pies[maxValue].cost && !onIncreace && maxValue>0)
    //        {
    //            maxValue--;
    //            onDecreace = true;
    //        }
    //        else if (onIncreace||onDecreace)
    //        {
    //            RefreshShop();
    //            break;
    //        }
    //        else
    //        {
    //            break;
    //        }

    //    } while (maxValue<pies.Count());
        
    //}

    private void RefreshShop()
    {
        if(lastChange == maxValue)
        {
            return;
        }
        else
        {
            lastChange = maxValue;
        }
        if (maxValue >= LevelManager.GetHighestLevel())
        {
            maxShop = LevelManager.GetHighestLevel();
        }
        else
            maxShop = maxValue;
        for(int i = 0; i < shopPies.Count; i++)
        {
            if (i < maxShop)
            {
                shopPies[i].GetComponent<ShopElement>().getBlockElement().SetActive(false);
                shopPies[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                shopPies[i].GetComponent<ShopElement>().getBlockElement().SetActive(true);
                shopPies[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void BuyPie(ShopElement element)
    {
        
        if (GoldManager.GetInstance().GetGold() > element.GetCost())
        {
            GoldManager.GetInstance().UpdateCurrentGold(-element.GetCost()); 
            levelManager.SpanwNewPie(element.GetLevelOfPie());
        }
     
    }
    public void Save()
    {

    }
    public void Load()
    {

    }
    void Start()
    {
        
        GenerateFirstShop();
    }
    private void GenerateFirstShop()
    {
        for (int i = 0; i < pies.Count(); i++)
        {
            GameObject gm = Instantiate(shopElement, shopCreator.transform);
            ShopElement shop = gm.GetComponent<ShopElement>();
            shop.SetName(pies[i].name);
            shop.SetCost(pies[i].cost);
            shop.SetSprite(pies[i].sprite);
            shop.SetLevelOfPie(pies[i].level);        
            gm.GetComponent<Button>().onClick.AddListener(()=>BuyPie(gm.GetComponent<ShopElement>()));
            shopPies.Add(gm);
        }
    }

}
