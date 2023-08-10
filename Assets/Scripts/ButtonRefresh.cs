using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRefresh : MonoBehaviour
{
    private Button PressButton;
    [SerializeField] private int timer;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider unavailableSlider;
    [SerializeField] private LevelManager levelManager;
    private int localTimer=0;
    private Image image;

    private void Awake()
    {
        LevelManager.NewCellsAvailable += CheckPies;
    }
    private void Start()
    {
        LevelManager.NewPieCreated += SetTimer;
       
        PressButton = GetComponent<Button>();
        image = GetComponent<Image>();
        unavailableSlider.value = 0;
        image.enabled = false;
    }

    private void CheckPies()
    {
        print("hi");
        if (localTimer <= 0)
        {
           
            levelManager.SpanwNewPie();
            SetTimer();
        }
    }

    private void SetTimer()
    {
       
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
            image.enabled = false;
            CancelInvoke(nameof(TickTimer));
            text.gameObject.SetActive(false);
            if (LevelManager.IsAvailableCells())
            {
                SetTimer();
                levelManager.SpanwNewPie();
                
            }
        }
    }
}
