using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour, ISaveable
{


    [SerializeField] private GameObject BackgroundPrefab;
    [SerializeField] private GameObject PiePrefab;

    [SerializeField] private GoldManager GoldManager;
    [SerializeField] private int BackgroundsCount;
    [SerializeField] private int PlayablePieCount;

    private static List<GameObject> BackgroundObjects = new List<GameObject>();
    private static List<GameObject> PieObjects = new List<GameObject>();


    private int matrixSize = 6;
    private GameObject[,] matrixOfPies;


    private int centerX;
    private int centerY;

    private int currentX;
    private int currentY;
    private int length = 0;
    private int direction = 0;
    private int increace = 2;
    private float spacing = 1.5f;
    private bool timeToIncrease = false;
    public void Save()
    {
        JSONObject save = new JSONObject();
        JSONArray arrayOfPies = new JSONArray();
        foreach (GameObject obj in PieObjects) 
        {
            JSONObject jsonItem = new JSONObject();
            SC_PieItem pieObj = obj.GetComponent<SC_PieItem>();
            jsonItem.Add("CellNum", new JSONNumber(pieObj.GetCell()));
            jsonItem.Add("PieIndex", new JSONNumber(pieObj.GetIndex()));
            jsonItem.Add("PieLeve", new JSONNumber(pieObj.GetPieLevel()));

            arrayOfPies.Add(jsonItem);
        }
        save.Add("BackgroundCount", BackgroundsCount);
        save.Add("PieList", arrayOfPies);
        SaveLoadHelp.saveFile.Add("LevelManager", save);
        
    }
    public void Load()
    {
        JSONObject saveData = new JSONObject();
        
        saveData.Add(SaveLoadHelp.saveFile["LevelManager"]);
        if (saveData != null)
        {
            BackgroundsCount = saveData["BackgroundCount"];
            
        }
        else {  }

    }


    private void Awake()
    {
        SaveLoadHelp.SubscribeSV(this.gameObject);
    }
    void Start()
    {

        centerX = matrixSize / 2;
        centerY = matrixSize / 2;
        currentX = centerX;
        currentY = centerY;
        matrixOfPies = new GameObject[matrixSize, matrixSize];
        if (SaveLoadHelp.continieGame == false)
        {

            StartCoroutine(GenerateStartField());
        }


    }

    public static void RemovePie(GameObject gm)
    {
        PieObjects.Remove(gm);
    }


    public void SpanwNewPie()
    {
        print(PieObjects.Count);
        foreach (GameObject SinglePie in PieObjects)
        {
            print(SinglePie.GetComponent<SC_PieItem>().GetPieLevel());
            if (SinglePie.GetComponent<SC_PieItem>().GetPieLevel() == -1)
            {
                SinglePie.GetComponent<SC_PieItem>().SpawnPie();
                break;
            }
        }
    }


    private IEnumerator GenerateStartField()
    {
        for (int currentBackground = 0; currentBackground < BackgroundsCount; currentBackground++)
        {
            if (CheckMatrixFilled())
            {
                Debug.Log("OVERFLOW");
            }
            else
            {
                GetNextCell();


            }

            yield return new WaitForSeconds(0f);


        }

        for (int currentPie = 0; currentPie < PlayablePieCount; currentPie++)
        {
            PieObjects[currentPie].GetComponent<SC_PieItem>().UpdatePieLevel();
        }

        
    }


   

    private bool CheckMatrixFilled()
    {

        for (int y = 0; y < matrixOfPies.GetLength(0); y++)
        {
            for (int x = 0; x < matrixOfPies.GetLength(1); x++)
            {
                if (!matrixOfPies[x, y])
                {
                    return false; 
                }
            }
        }

        return true; 
    }


    private void GetNextCell()
    {
       
        GameObject background = Instantiate(BackgroundPrefab);
        BackgroundObjects.Add(background);

        if (currentX >= 0 && currentX < matrixSize && currentY >= 0 && currentY < matrixSize)
        {
            
            if (matrixOfPies[currentX, currentY] == null)
            {

                background.transform.position = GetCellPosition(currentX, currentY);

               
                matrixOfPies[currentX, currentY] = background;
            }
            else
            {
                Debug.Log("Ячейка уже заполнена!");
            }
        }
        else
        {
            Debug.Log("Выход за пределы матрицы!");
        }


       


        float xPos = (currentX-2.5f) * spacing;
        float yPos = (currentY-2.5f) * spacing;
        background.transform.position = new Vector3((xPos), (yPos), 5);


        GetNextPie(background);
        UpdateCurrentCoordinates();


    }
    public void buyCells()
    {
        if (GoldManager.GetGold() >= Mathf.Pow(BackgroundsCount, 2))
        {
            GoldManager.UpdateCurrentGold(-BackgroundsCount * BackgroundsCount);


            GetNextCell();
        }
    }

    private void GetNextPie(GameObject background)
    {
        GameObject pieObj = Instantiate(PiePrefab);
        PieObjects.Add(pieObj);
        pieObj.transform.position = new Vector3(background.transform.position.x, background.transform.position.y, 3);
        SC_PieItem pieItem = pieObj.GetComponent<SC_PieItem>();
        pieItem.SetCell(PieObjects.IndexOf(pieObj));
        pieItem.ClearPie();
    }
    private Vector3 GetCellPosition(int x, int y)
    {

        float offsetX = 1.0f; 
        float offsetY = 1.0f;

        return new Vector3(x * offsetX, y * offsetY, 0f);
    }

 

    private void UpdateCurrentCoordinates()
    {

        switch (direction)
        {
         
            case 0: // Вниз
                currentY--;       
                break;
            case 1: // Влево
                currentX--;
                break;
            case 2: // Вверх
                currentY++;
                break;
            case 3: // Вправо
                currentX++;
                break;
        }


        if (BackgroundObjects.Count > 4)
        {
            increace--;
        }
        else if (BackgroundObjects.Count < 4)
        {
            direction++;
        }
        else if (BackgroundObjects.Count == 4)
        {
            currentX++;
            length = 2;
            direction = 0;
            timeToIncrease = true;
        }


        if (increace == 0) 
        {
            if (timeToIncrease)
            {
                length++;
                
                timeToIncrease = false;
            }
            else
            {
                timeToIncrease = true;
            }
            increace = length;
            direction++;
        }
        if(direction % 4 == 0) 
        {
            direction = 0;
        }
    }
}




/*
 # #
 # #

0 = 1 
1 = 2
2 = 4
3 = 8
4 = 16
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