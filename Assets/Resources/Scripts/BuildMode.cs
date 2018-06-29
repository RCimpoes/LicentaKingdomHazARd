using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildMode : MonoBehaviour
{

    public GameObject greenGrid;
    private bool lastValue = true;

    private void Start()
    {
          
    }

    void Update()
    {
        //// Check if the left mouse button was clicked
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // Check if the mouse was clicked over a UI element
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        Debug.Log("Clicked on the UI");
        //    }
        //}
    }


    public void HideShowGrid()
    {
        Debug.Log("Clicked button");

        if (EventSystem.current.IsPointerOverGameObject(-1))    // is the touch on the GUI
        {
            Debug.Log("current value:" + lastValue);
            greenGrid.SetActive(lastValue);
            lastValue = !lastValue;


        }
        // Your raycast code


    }
}
