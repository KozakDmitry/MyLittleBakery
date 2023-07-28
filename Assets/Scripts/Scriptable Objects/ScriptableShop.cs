using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/ShopData", order = 1)]
public class ScriptableShop : ScriptableObject
{
    public string nameElement;
    public Sprite sprite;
    public int level;

}
