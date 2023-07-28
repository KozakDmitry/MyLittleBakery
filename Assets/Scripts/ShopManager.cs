using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour,ISaveable
{
    [SerializeField] private ScriptableShop[] pies;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject shopElement;


    private void Awake()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
    }
    public void BuyPie(int pielevel)
    {
        
    }
    public void Save()
    {

    }
    public void Load()
    {

    }
    void Start()
    {
        
    }
    private void GenerateFirstShop()
    {
        for (int i = 0; i < LevelManager.GetHighestLevel()-3; i++)
        {
            GameObject gm = Instantiate(shopElement, this.transform);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
