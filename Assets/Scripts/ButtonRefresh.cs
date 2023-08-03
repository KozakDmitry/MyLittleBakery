using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRefresh : MonoBehaviour
{
    private Button PressButton;
    [SerializeField] private int timer;
    [SerializeField] private GameObject block;
    private int localTimer;
    private Image image;

    private void Start()
    {
        LevelManager.NewPieCreated += SetTimer;
        PressButton = GetComponent<Button>();
        image = GetComponent<Image>();
        image.enabled = false;
    }

    private void SetTimer()
    {

        localTimer = timer;
        block.transform.position = new Vector3(0, 0, 0);
        InvokeRepeating(nameof(TickTimer), 1f,1f);
        image.enabled = true;
    }
    public void TickTimer()
    {
        localTimer--;
        block.transform.position+= new Vector3(0, 0.1f, 0);
        if (localTimer <= 0)
        {
            image.enabled = false;
            CancelInvoke(nameof(TickTimer));
        }
    }
}
