using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.UIElements;

public class DragObjects : MonoBehaviour
{
    private Vector2 mousePosition;
    private float offestX, offestY;
    private bool mouseButtonIsPressed;
    private Vector3 startPosition;
    private static GameObject lastOverlappedPieObject,lastOverLappedBack;
    private static List<GameObject> allOverlappedPieObject = new List<GameObject>();
    private SC_PieItem currentPie;

    private void Start()
    {
        mouseButtonIsPressed = false;
        startPosition = transform.position;
        currentPie = GetComponent<SC_PieItem>();
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
            SC_PieItem overlapPie = lastOverlappedPieObject.GetComponent<SC_PieItem>();
            if (overlapPie.GetPieLevel() == currentPie.GetPieLevel())
            {    
                overlapPie.GetComponent<SC_PieItem>().UpdatePieLevel();    
                currentPie.ClearPie();
                lastOverlappedPieObject = null;
            }
            else if (overlapPie.GetPieLevel() == -1)
            {
                overlapPie.GetComponent<SC_PieItem>().SpawnPie(currentPie.GetPieLevel());
                currentPie.ClearPie();
                lastOverlappedPieObject = null;
            }

        }
        transform.position = startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mouseButtonIsPressed && collision.gameObject.TryGetComponent(out SC_PieItem isAObject))
        {
            if (isAObject.GetPieLevel() == gameObject.GetComponent<SC_PieItem>().GetPieLevel() || isAObject.GetPieLevel()==-1)
            {
                lastOverlappedPieObject = collision.gameObject;
                allOverlappedPieObject.Add(lastOverlappedPieObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (mouseButtonIsPressed && collision.gameObject.TryGetComponent(out SC_PieItem isAObject) && isAObject.GetPieLevel() == gameObject.GetComponent<SC_PieItem>().GetPieLevel())
        {
            allOverlappedPieObject.Remove(collision.gameObject);
            if (allOverlappedPieObject.Count > 0)
                lastOverlappedPieObject = allOverlappedPieObject.Last();
            else
                lastOverlappedPieObject = null;
        }
    }
}
