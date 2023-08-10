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
    [SerializeField] private ScrollRect shopList;
    private int maxValue=0, lastChange=-1;
    private List<GameObject> shopPies = new List<GameObject>();
 
    private void Awake()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
        GoldManager.GoldValueChanged += OnGoldValueChanged;  
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

    public void OpenShop()
    {
        shopList.verticalNormalizedPosition = 1;
        RefreshShop();
    }


    private void RefreshShop()
    {
        if (lastChange == maxValue)
        {
            return;
        }
        else
        {
            lastChange = maxValue;
        }
        for (int i = 0; i < LevelManager.GetHighestLevel(); i++)
        {
            if (i < maxValue)
            {
                shopPies[i].GetComponent<ShopElement>().SetAvailable(ShopElement.Available.Yes);
            }
            else
            {
                shopPies[i].GetComponent<ShopElement>().SetAvailable(ShopElement.Available.NoMoney);
            }
        }
    }

    public void BuyPie(ShopElement element)
    {
        if (GoldManager.GetInstance().GetGold() > element.GetCost() && LevelManager.IsAvailableCells())
        {
            levelManager.SpanwNewPie(element.GetLevelOfPie());
            GoldManager.GetInstance().UpdateCurrentGold(-element.GetCost()); 
            
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
        RefreshShop();
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
            shop.SetAvailable(ShopElement.Available.NoAccess);
            gm.GetComponent<Button>().onClick.AddListener(()=>BuyPie(gm.GetComponent<ShopElement>()));
            shopPies.Add(gm);
        }
    }

}
