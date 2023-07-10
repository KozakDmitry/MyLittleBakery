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

    private void OnMouseUp() 
    {
        mouseButtonIsPressed = false;
        if (lastOverlappedPieObject)
        {
            SC_PieItem overlapPie = lastOverlappedPieObject.GetComponent<SC_PieItem>();
            if (overlapPie.GetPieLevel() == currentPie.GetPieLevel())
            {
                lastOverlappedPieObject.GetComponent<SC_PieItem>().UpdatePieLevel();
                currentPie.ClearPie();
                lastOverlappedPieObject = null;
            }
        }
        transform.position = startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mouseButtonIsPressed && collision.gameObject.TryGetComponent(out SC_PieItem isAPie) && isAPie.GetPieLevel() == gameObject.GetComponent<SC_PieItem>().GetPieLevel())
        {
            lastOverlappedPieObject = collision.gameObject;
            allOverlappedPieObject.Add(lastOverlappedPieObject);
            print("Enter " + collision.gameObject.name.ToString());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (mouseButtonIsPressed && collision.gameObject.TryGetComponent(out SC_PieItem isAPie) && isAPie.GetPieLevel() == gameObject.GetComponent<SC_PieItem>().GetPieLevel())
        {

            allOverlappedPieObject.Remove(collision.gameObject);
            if (allOverlappedPieObject.Count > 0)
                lastOverlappedPieObject = allOverlappedPieObject.Last();
            else
                lastOverlappedPieObject = null;
            print("Exit " + collision.gameObject);
        }
    }
}
