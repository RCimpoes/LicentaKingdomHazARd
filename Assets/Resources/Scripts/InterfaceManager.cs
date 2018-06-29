using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaceManager : MonoBehaviour {

    public GameObject grid;
    public GameObject buildingPanel;
    public GameObject viewMode;
    public GameObject buildMode;

    public static bool confirmationHasChanged = false;
    public static bool confirmationValue;




    public bool wallToggleState = true;


    private bool lastValue = true;


    private int fingerID = -1;

    public void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            fingerID = 0;
        }


    }

    public void HideShowGrid()
    {
       
        if (EventSystem.current.IsPointerOverGameObject(fingerID))    // is the touch on the GUI
        {
           
            grid.SetActive(lastValue);
            viewMode.SetActive(!lastValue);
            buildMode.SetActive(lastValue);
            buildingPanel.SetActive(lastValue);
            lastValue = !lastValue;
           
        }


      

    }

    public void wallTogleChanged()
    {
        wallToggleState = !wallToggleState;
    }

    public void ConfirmationClikedYes()
    {

        confirmationHasChanged = true;
        confirmationValue = true;
    }

    public void ConfirmationClikedNo()
    {
        confirmationHasChanged = true;
        confirmationValue = false;

    }




}
