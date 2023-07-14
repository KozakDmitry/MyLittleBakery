using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SC_LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject[] BackgroundPosistions;

    [SerializeField] private GameObject BackgroundPrefab;
    [SerializeField] private GameObject PiePrefab;
    [SerializeField] private GameObject CurrentGoldText;
    [SerializeField] private GameObject ExperienceText;

    [SerializeField] private int StartBackgroundsCount;
    [SerializeField] private int PlayablePieCount;

    private static List<GameObject> BackgroundObjects = new List<GameObject>();
    private static List<GameObject> PieObjects = new List<GameObject>();
    private SC_PieItem pieScript; 
    private static GameObject GoldCurrentTextObject;
    private static GameObject ExperienceTextObject;
    private float currentGold;
    private float currentExperience;

    private int matrixSize = 6;
    private GameObject[,] matrixOfPies;
<<<<<<< Updated upstream
=======


    private int centerX;
    private int centerY;

    private int currentX;
    private int currentY;
    private int length = 0;
    private int direction = 0;
<<<<<<< Updated upstream
    private int increace = 3;
    private float spacing = 1.5f;
>>>>>>> Stashed changes
=======
    private int increace = 2;
    private int currentBackground;
    private float spacing = 1.5f;
    private bool timeToIncrease = false;

    private string arrayString;
 

    public void Save()
    {
       
        PlayerPrefs.SetInt("CountOfCells", StartBackgroundsCount) ;
        List<int> instancesIDs = new List<int>(); 
        foreach (GameObject obj in PieObjects) 
        {
            instancesIDs.Add(obj.GetInstanceID());
        }
        arrayString = string.Join(",", instancesIDs);
        PlayerPrefs.SetString("SavedPies", arrayString);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        if (PlayerPrefs.GetString("SavedPies") != null)
        {
            arrayString = PlayerPrefs.GetString("SavedPies");
            string[] stringArray = arrayString.Split(',');
            PieObjects = new List<GameObject>();

            foreach (string str in stringArray) 
            {
                int instanceID;
                if(int.TryParse(str, out instanceID))
                {
                    
                }
            }
        }
    }
>>>>>>> Stashed changes
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
        matrixOfPies = new GameObject[matrixSize, matrixSize];
        GoldCurrentTextObject = CurrentGoldText;
        ExperienceTextObject = ExperienceText;

<<<<<<< Updated upstream
        for (int currentBackground = 0; currentBackground < BackgroundsCount; currentBackground++)
=======
    private IEnumerator GenerateStartField()
    {
        for (currentBackground = 0; currentBackground < StartBackgroundsCount; currentBackground++)
>>>>>>> Stashed changes
        {
            GameObject backgroundObj = Instantiate(BackgroundPrefab);
            BackgroundObjects.Add(backgroundObj);
            backgroundObj.transform.position = new Vector3(BackgroundPosistions[currentBackground].transform.position.x, BackgroundPosistions[currentBackground].transform.position.y, 10);

            GameObject pieObj = Instantiate(PiePrefab);
            PieObjects.Add(pieObj);
            FillMatrix(pieObj);
            pieObj.transform.position = new Vector3(BackgroundPosistions[currentBackground].transform.position.x, BackgroundPosistions[currentBackground].transform.position.y, 5);
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
<<<<<<< Updated upstream
        if (currentGold > 0)
        {

=======
        if (currentGold >= StartBackgroundsCount*StartBackgroundsCount)
        {
            currentGold -= StartBackgroundsCount*StartBackgroundsCount;

            currentBackground++;
            GetNextCell();
>>>>>>> Stashed changes
        }
    }

    private void FillMatrix(GameObject pie)
    {
       

        int centerX = matrixSize / 2;
        int centerY = matrixSize / 2;

        int currentX = centerX;
        int currentY = centerY;
        int length = 1;
        int direction = 0; 

        while (length < matrixSize)
        {
            for (int i = 0; i < length; i++)
            {
                matrixOfPies[currentX, currentY] = pie;

                switch (direction)
                {
                    case 0: 
                        currentX++;
                        break;
                    case 1: 
                        currentY--;
                        break;
                    case 2: 
                        currentX--;
                        break;
                    case 3: 
                        currentY++;
                        break;
                }
            }
<<<<<<< Updated upstream
            pie.transform.position = new Vector3((currentX), (currentY), 0);
            direction = (direction + 1) % 4; 

            if (direction == 0 || direction == 2)
                length++; 
        }

       
=======
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



        float xPos = (currentX+length) * spacing;
        float yPos = (currentY+length) * spacing;

        background.transform.position = new Vector3((xPos), (yPos), 5);


        
        GameObject pieObj = Instantiate(PiePrefab);
        PieObjects.Add(pieObj);
        pieScript = pieObj.GetComponent<SC_PieItem>();
        pieScript.setNum(currentBackground); 
        pieObj.transform.position = new Vector3(background.transform.position.x, background.transform.position.y, 3);
        pieObj.GetComponent<SC_PieItem>().SetLevelManager(gameObject);
        pieObj.GetComponent<SC_PieItem>().ClearPie();

        Debug.Log(length);
        UpdateCurrentCoordinates();


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
                length = -length;
                break;
            case 2: // Вверх
                currentY++;
                break;
            case 3: // Вправо
                currentX++;
                length = -length;
                break;
        }

        direction++;
        increace--;
        if(increace == 0) 
        {
            length = length >= 0 ? length++ : length--;
            increace = 2;
        }
        if(direction % 4 == 0) 
        {
            direction = 0;
        }
>>>>>>> Stashed changes
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