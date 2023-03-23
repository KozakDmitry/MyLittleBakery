using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragObjects : MonoBehaviour
{
    private Vector2 mousePosition;
    private float offestX, offestY;
    private bool mouseButtonIsPressed;
    private Vector3 startPosition;
    private static GameObject overlappedPieObject;
    private SC_PieItem currentPie;

    private void Start()
    {
        mouseButtonIsPressed = false;
        startPosition = transform.position;
        currentPie = GetComponent<SC_PieItem>();
    }

    private void OnMouseDown() // called when object is selected
    {
        if (currentPie.GetPieLevel() >= 0)
        {
            mouseButtonIsPressed = true;
            offestX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            offestY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
    }

    private void OnMouseDrag() // called when object is moving 
    {
        if (mouseButtonIsPressed)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - offestX, mousePosition.y - offestY);
        }
    }

    private void OnMouseUp() // called when object stops to be selected
    {
        mouseButtonIsPressed = false;
        if (overlappedPieObject)
        {
            SC_PieItem overlapPie = overlappedPieObject.GetComponent<SC_PieItem>();
            if (overlapPie.GetPieLevel() == currentPie.GetPieLevel())
            {
                overlappedPieObject.GetComponent<SC_PieItem>().UpdatePieLevel();
                currentPie.ClearPie();
                overlappedPieObject = null;
            }
        }
        transform.position = startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mouseButtonIsPressed && collision.gameObject.TryGetComponent(out SC_PieItem isAPie))
        {
            overlappedPieObject = collision.gameObject;
            print("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        overlappedPieObject = null;
        print("Exit");
    }
}
