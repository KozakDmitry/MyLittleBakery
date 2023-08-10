using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour, ISaveable
{


    [SerializeField] private GameObject BackgroundPrefab;

    [SerializeField] private GoldManager GoldManager;
    [SerializeField] private int BackgroundsCount;

    private static List<GameObject> BackgroundObjects = new List<GameObject>();
    private static List<GameObject> PieObjects = new List<GameObject>();




    public delegate void ValueChangedEventHandler();
    public delegate void ValueChangedEvent();
    public static event ValueChangedEvent NewPieCreated;
    public static event ValueChangedEventHandler NewCellsAvailable;

    private int matrixSize = 6;
    private GameObject[,] matrixOfPies;


    private int centerX, centerY;
    private static int highestLevel = -1;

    private int currentX, currentY;
    private int length = 0;
    private int direction = 0;
    private int increace = 2;
    private int numToAdapt = 9;
    private float spacing = 1.5f, size = 1, deacreaseSize = 0.2f;
    private bool timeToIncrease = false;
    private static bool isAvailableCells = false;
    public void Save()
    {
        JSONObject save = new JSONObject();
        JSONArray arrayOfPies = new JSONArray();
        foreach (GameObject obj in PieObjects)
        {
            JSONObject jsonItem = new JSONObject();
            PieItem pieObj = obj.GetComponent<PieItem>();
            jsonItem.Add("CellNum", new JSONNumber(pieObj.GetCell()));
            jsonItem.Add("PieIndex", new JSONNumber(pieObj.GetIndex()));
            jsonItem.Add("PieLeve", new JSONNumber(pieObj.GetPieLevel()));

            arrayOfPies.Add(jsonItem);
        }
        save.Add("BackgroundCount", BackgroundsCount);
        save.Add("PieList", arrayOfPies);
        SaveLoadHelp.saveFile.Add("LevelManager", save);

    }

    public static void SetAvailableCells(bool set)
    {
        isAvailableCells = set;
        if (set)
        {
           NewCellsAvailable();
        }
    }
    public static bool IsAvailableCells()
    {
        return isAvailableCells;
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

            GenerateStartField();
        }


    }

    public static void RemovePie(GameObject gm)
    {
        PieObjects.Remove(gm);
    }

    private List<int> AvailableCells()
    {
        List<int> cells = new List<int>();

        foreach (GameObject SinglePie in PieObjects)
        {
            if (SinglePie.GetComponent<PieItem>().GetPieLevel() == -1)
            {
                
                cells.Add(PieObjects.IndexOf(SinglePie));
            }

        }
       
        return cells;
        
    }

    public void SpanwNewPie(int pieLevel =0)
    {
        List<int> cells = AvailableCells();
        if (cells.Count > 0)
        {

            PieObjects[cells.ElementAt(Random.Range(0, cells.Count))].GetComponent<PieItem>().SpawnPie(pieLevel);
            NewPieCreated();
            if (cells.Count > 1)
            {
                SetAvailableCells(true);
            }    
        }
        else
        {
            SetAvailableCells(false);
            Debug.Log("NO ENOUGH");
      
        }
        
    }
    public static void CheckHighestLevel(int high)
    {
        if (high > highestLevel)
        {
            highestLevel = high;
        }        
    }

    public static int GetHighestLevel()
    {
        return highestLevel;
    }
    private void AdaptPies()
    {

        if (BackgroundObjects.Count > numToAdapt)
        {
            size-= deacreaseSize;
            spacing -= deacreaseSize;
            foreach (GameObject item in BackgroundObjects)
            {
                item.transform.localScale *= size;
                item.transform.position *= (1/(spacing+deacreaseSize))*spacing;
                item.GetComponentInChildren<DragObjects>().UpdatePosition();
            }
            
            numToAdapt = Extentions.Pow(((int)Mathf.Sqrt(numToAdapt) + 1), 2);
        }
    }

    private void GenerateStartField()
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



        }

        for (int currentPie = 0; currentPie < BackgroundsCount; currentPie++)
        {
            PieObjects[currentPie].GetComponent<PieItem>().SpawnPie();
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


       


        float xPos = (currentX-3f) * spacing;
        float yPos = (currentY-3f) * spacing;
        background.transform.localScale *= size;
        background.transform.position = new Vector3((xPos), (yPos), 5);


        PieItem pieItem = background.transform.GetChild(0).gameObject.GetComponent<PieItem>();
        PieObjects.Add(background.transform.GetChild(0).gameObject); 
        pieItem.SetCell(PieObjects.IndexOf(background.transform.GetChild(0).gameObject));
        pieItem.ClearPie();

        AdaptPies();
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