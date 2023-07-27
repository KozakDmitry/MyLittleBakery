using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour,ISaveable
{
    [SerializeField] private PieItem[] pies;
    [SerializeField] private LevelManager levelManager;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
