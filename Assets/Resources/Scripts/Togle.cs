using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Togle : MonoBehaviour
{

    public GameObject element;
    private bool lastBool = false;

    private void Start()
    {
        element.SetActive(lastBool);
    }

    public void InverseVisibility()
    {
        lastBool = !lastBool;
        element.SetActive(lastBool);
    }

}
