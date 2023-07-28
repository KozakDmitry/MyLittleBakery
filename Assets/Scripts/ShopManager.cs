using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour,ISaveable
{
    [SerializeField] private ScriptableShop[] pies;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject shopElement;


    private void Awake()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
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
        for (int i = 0; i < LevelManager.GetHighestLevel()-3; i++)
        {
            GameObject gm = Instantiate(shopElement, this.transform);
            gm.GetComponent<Button>().onClick.AddListener(()=>BuyPie(gm.GetComponent<ShopElement>()));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
