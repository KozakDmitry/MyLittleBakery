using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SC_LevelManager : MonoBehaviour
{


    [SerializeField] private GameObject BackgroundPrefab;
    [SerializeField] private GameObject PiePrefab;
    [SerializeField] private GameObject CurrentGoldText;
    [SerializeField] private GameObject ExperienceText;

    [SerializeField] private int BackgroundsCount;
    [SerializeField] private int PlayablePieCount;

    private static List<GameObject> BackgroundObjects = new List<GameObject>();
    private static List<GameObject> PieObjects = new List<GameObject>();

    private static GameObject GoldCurrentTextObject;
    private static GameObject ExperienceTextObject;
    private float currentGold;
    private float currentExperience;

    private int matrixSize = 6;
    private GameObject[,] matrixOfPies;


    private int centerX;
    private int centerY;

    private int currentX;
    private int currentY;
    private int length = 0;
    private int direction = 0;

    private float spacing = 1.5f;
    public void UpdateCurrentGold(float goldToAdd)
    {
        currentGold += goldToAdd;
        GoldCurrentTextObject.GetComponent<Text>().text = currentGold.ToString();
    }

    public void UpdateCurrentExperience(float expToAdd)
    {
        currentExperience += expToAdd;
        ExperienceTextObject.GetComponent<Text>().text = currentExperience.ToString();
    }

    public void SpanwNewPie()
    {
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

    // Start is called before the first frame update
    void Start()
    {
        centerX = matrixSize / 2;
        centerY = matrixSize / 2;
        currentX = centerX;
        currentY = centerY;

        matrixOfPies = new GameObject[matrixSize, matrixSize];
        GoldCurrentTextObject = CurrentGoldText;
        ExperienceTextObject = ExperienceText;

        for (int currentBackground = 0; currentBackground < BackgroundsCount; currentBackground++)
        {
            GameObject backgroundObj = Instantiate(BackgroundPrefab);
            BackgroundObjects.Add(backgroundObj);
            if (CheckMatrixFilled())
            {
                Debug.Log("OVERFLOW");
            }
            else
            {
                if (length != matrixOfPies.GetLength(0))
                {
                    backgroundObj.transform.position = GetNextCell(backgroundObj);
                }
            }

           

            GameObject pieObj = Instantiate(PiePrefab);
            PieObjects.Add(pieObj);
            pieObj.transform.position = new Vector3(backgroundObj.transform.position.x, backgroundObj.transform.position.y, 3);
            pieObj.GetComponent<SC_PieItem>().SetLevelManager(gameObject);
            pieObj.GetComponent<SC_PieItem>().ClearPie();
        }

        for (int currentPie = 0; currentPie < PlayablePieCount; currentPie++)
        {
            PieObjects[currentPie].GetComponent<SC_PieItem>().UpdatePieLevel();
        }
    }



    public void buyCells()
    {
        if (currentGold > 0)
        {

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
    private Vector3 GetNextCell(GameObject background)
    {

      
        if (currentX >= 0 && currentX < matrixSize && currentY >= 0 && currentY < matrixSize)
        {
            
            if (matrixOfPies[currentX, currentY] == null)
            {
           
                GameObject newObject = Instantiate(background, transform);
                newObject.transform.position = GetCellPosition(currentX, currentY);

               
                matrixOfPies[currentX, currentY] = newObject;
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

    
        UpdateCurrentCoordinates();

  
        if (length == 0)
        {
            direction = (direction + 1) % 4; 
            if (direction == 0 || direction == 2)
            {
                length++; 
            }
        }

     
        length--;
       
        

        direction = (direction + 1) % 4;

        if (direction == 0 || direction == 2)
        {
            length++;
        }
        if (matrixOfPies[currentX, currentY] == null)
        {
            matrixOfPies[currentX, currentY] = background;
        }
    
        float xPos = (length + currentX + background.transform.localScale.x-4.5f) * spacing;
        float yPos = (length + currentY + background.transform.localScale.y-4.5f) * spacing;
  
        return new Vector3((xPos), (yPos), 5);
       

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
            case 0: // Вправо
                currentX++;
                break;
            case 1: // Вниз
                currentY++;
                break;
            case 2: // Влево
                currentX--;
                break;
            case 3: // Вверх
                currentY--;
                break;
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