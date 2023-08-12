using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRefresh : MonoBehaviour
{
    private Button pressButton;

    [SerializeField] private Button spawnPieButton;
    [SerializeField] private int timer;
    [SerializeField] private TextMeshProUGUI text,fullText;
    [SerializeField] private Slider unavailableSlider;
    [SerializeField] private LevelManager levelManager;
    private int localTimer=0;
    private Image image;
    private bool onCountdown = false;

    private void Awake()
    {
        pressButton = GetComponent<Button>();
        image = GetComponent<Image>();
        unavailableSlider.value = 0;
        image.enabled = false;
        SetTimer();
        LevelManager.NewPieCreated += SetTimer;
        LevelManager.NewCellsAvailable += CheckPies;
    }


    private void Start()
    {
        
       
    }

    private void CheckPies()
    {
        if (!onCountdown)
        {

            SetTimer();
            levelManager.SpanwNewPie();
            
        }
    }

    private void SetTimer()
    {
      
        if (onCountdown)
        {
            return;
        }
        onCountdown = true;
        fullText.gameObject.SetActive(false);
        spawnPieButton.interactable = true;
        localTimer = timer;    
        text.text = localTimer.ToString();
        text.gameObject.SetActive(true);
        unavailableSlider.value = 1;
        InvokeRepeating(nameof(TickTimer), 1f,1f);
        image.enabled = true;
    }
    public void TickTimer()
    {
        localTimer--;
        unavailableSlider.value -= 0.1f;
        text.text = localTimer.ToString();
        if (localTimer <= 0)
        {
            onCountdown = false;
            image.enabled = false;
            CancelInvoke(nameof(TickTimer));
            text.gameObject.SetActive(false);          
            if (LevelManager.IsAvailableCells())
            {
                SetTimer();
                levelManager.SpanwNewPie();        
            }
            else
            {
                spawnPieButton.interactable = false;
                fullText.gameObject.SetActive(true);
                
            }
        }
    }
}
