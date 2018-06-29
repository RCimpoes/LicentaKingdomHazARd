using System.Collections.Generic;
using UnityEngine;

public class TutorialLogic : MonoBehaviour
{
    public List<GameObject> ListOfPanels;
    public GameObject ExteriorOverlay;
    public GameObject UIOverlay;
    public GameObject PausePanel;
    public static bool TutorialPassed;

    private static int currentPanel;
    private static bool shouldStartTimer;

    private float counterTime;
    public void NextPanel()
    {
        if (!TutorialPassed)
        {
            switch (currentPanel)
            {
                case 4:
                    ListOfPanels[currentPanel - 1].SetActive(false);
                    shouldStartTimer = true;
                    UIOverlay.SetActive(false);
                    break;
                case 10:
                    ListOfPanels[currentPanel - 1].SetActive(false);
                    currentPanel++;
                    break;
                case 11:
                    enemyControll.canStartGame = true;
                    PausePanel.SetActive(true);
                    if (!TutorialPassed)
                    {
                        TutorialPassed = true;
                    }
                    break;
                default:
                    ListOfPanels[currentPanel - 1].SetActive(false);
                    ListOfPanels[currentPanel].SetActive(true);
                    currentPanel++;
                    break;
            }
        }
        else
        {
            shouldStartTimer = true;
        }
       
    }

    private void Awake()
    {
        TutorialPassed = false;
    }

    private void Start()
    {
        currentPanel = 1;
        counterTime = 1f;
        shouldStartTimer = false;
        ListOfPanels[0].SetActive(true);
        ExteriorOverlay.SetActive(true);
        UIOverlay.SetActive(true);
        PausePanel.SetActive(false);

    }

    void Update()
    {
        if (currentPanel == 11)
        {
            enemyControll.canStartGame = true;
            ExteriorOverlay.SetActive(false);
            UIOverlay.SetActive(false);
            PausePanel.SetActive(true);
            currentPanel++;
        }
        if (shouldStartTimer)
        {
            counterTime -= Time.deltaTime;
            if (counterTime <= 0)
            {
                shouldStartTimer = false;
                ListOfPanels[currentPanel].SetActive(true);
                currentPanel++;
                UIOverlay.SetActive(true);
            }
        }
    }
}
