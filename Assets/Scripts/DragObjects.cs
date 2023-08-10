using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DragObjects : MonoBehaviour
{
    private Vector2 mousePosition;
    private float offestX, offestY;
    private bool mouseButtonIsPressed;
    private Vector3 startPosition;
    private static GameObject lastOverlappedPieObject;
    private static List<GameObject> allOverlappedPieObject = new List<GameObject>();
    private PieItem currentPie;

    private void Start()
    {
        mouseButtonIsPressed = false;
        startPosition = transform.position;
        currentPie = GetComponent<PieItem>();
    }
    public void UpdatePosition()
    {
        startPosition = transform.position;
    }
    private void OnMouseDown() 
    {
        if (currentPie.GetPieLevel() >= 0)
        {
            mouseButtonIsPressed = true;
            offestX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            offestY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
    }

    private void OnMouseDrag() 
    {
        if (mouseButtonIsPressed)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - offestX, mousePosition.y - offestY);
        }
    }

    private void OnMouseUp() 
    {
        mouseButtonIsPressed = false;
        if (lastOverlappedPieObject)
        {
            PieItem overlapPie = lastOverlappedPieObject.GetComponent<PieItem>();
            if (overlapPie.GetPieLevel() == currentPie.GetPieLevel())
            {
                overlapPie.GetComponent<PieItem>().UpdatePieLevel();    
                currentPie.ClearPie();
                lastOverlappedPieObject = null;
                LevelManager.SetAvailableCells(true);
            }
            else if (overlapPie.GetPieLevel() == -1)
            {
                overlapPie.GetComponent<PieItem>().SpawnPie(currentPie.GetPieLevel());
                currentPie.ClearPie();
                lastOverlappedPieObject = null;
            }

        }
        transform.position = startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mouseButtonIsPressed && collision.gameObject.TryGetComponent(out PieItem isAObject))
        {
            if (isAObject.GetPieLevel() == gameObject.GetComponent<PieItem>().GetPieLevel() || isAObject.GetPieLevel()==-1)
            {
                lastOverlappedPieObject = collision.gameObject;
                allOverlappedPieObject.Add(lastOverlappedPieObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (mouseButtonIsPressed && collision.gameObject.TryGetComponent(out PieItem isAObject) && isAObject.GetPieLevel() == gameObject.GetComponent<PieItem>().GetPieLevel())
        {
            allOverlappedPieObject.Remove(collision.gameObject);
            if (allOverlappedPieObject.Count > 0)
                lastOverlappedPieObject = allOverlappedPieObject.Last();
            else
                lastOverlappedPieObject = null;
        }
    }
}
