using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AddButton : MonoBehaviour
{

    [SerializeField] GameObject LevelManagetObject;
    private static SC_LevelManager LevelManagerScript;
    // Start is called before the first frame update

    private void Start()
    {
        LevelManagerScript = LevelManagetObject.GetComponent<SC_LevelManager>();    
    }

    private void OnMouseDown()
    {
        LevelManagerScript.SpanwNewPie();
    }
}
