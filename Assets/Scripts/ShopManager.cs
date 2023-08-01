using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour,ISaveable
{
    [SerializeField] private ScriptableShop[] pies;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject shopElement;
    private int maxValue;
    private bool onDecreace = false,onIncreace = false;
    private List<GameObject> shopPies = new List<GameObject>();

    private void Awake()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
        GoldManager.GoldValueChanged += OnGoldValueChanged;
    }

    private void OnGoldValueChanged(float newValue)
    {
        do
        {
            if (newValue > pies[maxValue].cost && !onDecreace && maxValue<=pies.Count())
            {
                maxValue++;
                onIncreace = true;
            }
            else if (newValue < pies[maxValue].cost&&!onIncreace && maxValue>=0)
            {
                maxValue--;
                onDecreace = true;
            }
            else if (onIncreace||onDecreace)
            {
                RefreshShop();
                break;
            }
            else
            {
                break;
            }

        } while (maxValue<pies.Count());
        
    }

    private void RefreshShop()
    {
        int maxShop;
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
            }
            else
            {
                shopPies[i].GetComponent<ShopElement>().getBlockElement().SetActive(true);
            }
        }
    }

    public void BuyPie(ShopElement element)
    {
        if (GoldManager.GetInstance().GetGold() > element.GetCost())
        {
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
    }
    private void GenerateFirstShop()
    {
        for (int i = 0; i < pies.Count(); i++)
        {
            GameObject gm = Instantiate(shopElement, this.transform);
            gm.GetComponent<Button>().onClick.AddListener(()=>BuyPie(gm.GetComponent<ShopElement>()));
            shopPies.Add(gm);
        }
    }

}
