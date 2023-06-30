using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SC_Background : MonoBehaviour
{

    [SerializeField]
    private int number; 
    public SC_Background(int number)
    {
        this.number = number;
    }
}
