using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
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
    private int cellCount = 4;


    private int centerX;
    private int centerY;

    private int currentX;
    private int currentY;
    private int length = 1;
    private int direction = 0;
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
            backgroundObj.transform.position = FillMatrix(backgroundObj);
            Debug.Log(backgroundObj.transform.position);
            //backgroundObj.transform.position = new Vector3(BackgroundPosistions[currentBackground].transform.position.x, BackgroundPosistions[currentBackground].transform.position.y, 10);

            GameObject pieObj = Instantiate(PiePrefab);
            PieObjects.Add(pieObj);
            //FillMatrix(pieObj);
            pieObj.transform.position = new Vector3(backgroundObj.transform.position.x, backgroundObj.transform.position.y, 5);
            //pieObj.transform.position = new Vector3(backgroundObj.transform.position.x, backgroundObj.transform.position.y, 5);
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

    private Vector3 FillMatrix(GameObject background)
    { 
            for (int i = 0; i < length; i++)
            {
                matrixOfPies[currentX, currentY] = background;

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

            direction = (direction + 1) % 4;

            if (direction == 0 || direction == 2)
                length++;

            BackgroundsCount++;
            return new Vector3((currentX), (currentY), 0);
           
   
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