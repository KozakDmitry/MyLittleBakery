using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SC_LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject[] BackgroundPosistions;

    [SerializeField] private GameObject BackgroundPrefab;
    [SerializeField] private GameObject PiePrefab;
    [SerializeField] private int BackgroundsCount;
    [SerializeField] private int PieCount;

    private List<GameObject> BackgroundObjects = new List<GameObject>();
    private List<GameObject> PieObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int currentBackground = 0; currentBackground < BackgroundsCount; currentBackground++)
        {
            BackgroundObjects.Add(BackgroundPrefab);
            GameObject backgroundObj = Instantiate(BackgroundObjects[currentBackground]);
            backgroundObj.transform.position = new Vector3(BackgroundPosistions[currentBackground].transform.position.x, BackgroundPosistions[currentBackground].transform.position.y, 10);
        }

        for(int currentPie = 0; currentPie < PieCount; currentPie++)
        {
            PieObjects.Add(PiePrefab);
            GameObject pieObj = Instantiate(PieObjects[currentPie]);
            pieObj.transform.position = new Vector3(BackgroundPosistions[currentPie].transform.position.x, BackgroundPosistions[currentPie].transform.position.y, 5);
        }
    }
}

/*
 # #
 # #
 
   
 # # #
 # # 

 # # #
 # # #

   # 
 # # #
 # # #

 # # 
 # # #
 # # #

 # # #
 # # # 
 # # #
 */