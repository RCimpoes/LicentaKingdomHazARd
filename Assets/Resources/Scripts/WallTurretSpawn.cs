using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WallTurretSpawn : MonoBehaviour
{

    public GameObject turretPrefab;
    public GameObject wallPrefab;
    public GameObject particleDestroy;
    public GameObject denyPanel;
    public Toggle wallToggle;
    public Toggle towerToggle;
    public GameObject spawnGrid;
    public GameObject confirmPanel;
    public GameObject upgradePanel;
    public Transform particleSpawnPoint;
    public Transform panelSpawnPoint;// vezi ce i cu asta
    public List<GameObject> interactablePlanes;
    public List<GameObject> spawnPlanes;


    private int fingerID;
    private int goldPerUnit;
    private GameObject objectToDestroy;//object that eventually blocks the path
    private GameObject objectToBeBuild;
    private GameObject objectToRemove;//object that user clicks and wants it out of the scene
    private CurrencyLogic currencyLogic;
    private bool agentsCanReachTarget;
    private GameObject[] allAgents;
    private GameObject ArCamera;

    void Start()
    {
        objectToBeBuild = wallPrefab;
        objectToDestroy = null;
        currencyLogic = CurrencyLogic.getinstance();

        if (Application.platform == RuntimePlatform.Android)
        {
            fingerID = 0;
        }
        else
        {
            fingerID = -1;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (UserConfirmsDecision())
        {
            Destroy(objectToRemove);
            if (objectToRemove.transform.name == wallPrefab.transform.name + "(Clone)")
            {
                int ammountToAdd = currencyLogic.GetWallCost() / 2;
                currencyLogic.AddAmountToCurrency(ammountToAdd);
            }
            else if (objectToRemove.transform.name == turretPrefab.transform.name + "(Clone)")
            {
                int ammountToAdd = currencyLogic.GetTurretCost() / 2;
                currencyLogic.AddAmountToCurrency(ammountToAdd);
            }
            InterfaceManager.confirmationHasChanged = false;
            InterfaceManager.confirmationValue = false;
            confirmPanel.SetActive(false);
            upgradePanel.SetActive(false);
            objectToRemove = null;
        }

        if (objectToDestroy != null)
        {
            agentsCanReachTarget = AgentControl.canReachTarget; // replace this with better decision taking 
            if (!agentsCanReachTarget)
            {
                particleSpawnPoint = objectToDestroy.transform.GetChild(0);
                panelSpawnPoint = objectToDestroy.transform.GetChild(1);
                GameObject effectDestroyInstance = Instantiate(particleDestroy, particleSpawnPoint.position, particleSpawnPoint.rotation);
                GameObject objectPanel = Instantiate(denyPanel, panelSpawnPoint.position, panelSpawnPoint.rotation);

                ArCamera = GameObject.Find("ARCamera");

                objectPanel.transform.LookAt(ArCamera.transform);
                float tempX = objectPanel.transform.rotation.x;
                float tempY = objectPanel.transform.rotation.y;
                float tempZ = objectPanel.transform.rotation.z;
                float tempW = objectPanel.transform.rotation.w;

                objectPanel.transform.rotation = new Quaternion(0, tempY, 0, tempW);


                Destroy(effectDestroyInstance, 2f);
                Destroy(objectPanel, 2.2f);
                Destroy(objectToDestroy);

                Debug.Log("Can't build wall here!");
            }
            else
            {
                objectToDestroy.SetActive(true);
            }
        }

        DetermineObjectToBuild();
        if (clickedOrTouched())
        {
            if (!EventSystem.current.IsPointerOverGameObject(fingerID))
            {
                RaycastHit hit;
                Ray ray;

                if (Application.platform == RuntimePlatform.Android)
                {
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                }
                else
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                }


                if (Physics.Raycast(ray, out hit, Mathf.Infinity) && spawnGrid.activeSelf)
                {
                    if (hit.transform.name == "WallTower(Clone)" || hit.transform.name == "WallBlockade(Clone)")
                    {
                        confirmPanel.SetActive(true);
                        if (hit.transform.name == "WallTower(Clone)")
                        {
                            upgradePanel.SetActive(true);
                            UpdateLogic.targetObject = hit.transform.gameObject;
                        }
                        objectToRemove = hit.transform.gameObject;

                    }
                    else if (hit.transform.name == "LumberJack(Clone)")
                    {
                        upgradePanel.SetActive(false);
                        confirmPanel.SetActive(false);
                        Destroy(hit.transform.gameObject);//adjust to just take damage

                        currencyLogic.AddAmountToCurrency(goldPerUnit);
                        currencyLogic.AddKill();
                        currencyLogic.DecreaseAlive();
                    }
                    else
                    {
                        upgradePanel.SetActive(false);
                        confirmPanel.SetActive(false);

                        for (int index = 0; index < interactablePlanes.Count; index++)
                        {
                            if (interactablePlanes[index].transform.name == hit.transform.name)
                            {
                                Plane tile = interactablePlanes[index].GetComponent<Plane>();
                                if (!tile.isDisabled)
                                {
                                    GameObject currentSpawnPoint = spawnPlanes[index];

                                    if (objectToBeBuild == wallPrefab)
                                    {
                                        if (currencyLogic.canBuyWall())
                                        {
                                            currencyLogic.BuyWall();
                                            GameObject objectBuildGO = Instantiate(objectToBeBuild, currentSpawnPoint.transform);

                                            ForceUpdatePath();
                                            objectBuildGO.SetActive(false);
                                            objectToDestroy = objectBuildGO;


                                        }
                                    }
                                    else if (objectToBeBuild == turretPrefab)
                                    {

                                        if (currencyLogic.canBuyTurret())
                                        {
                                            currencyLogic.BuyTurret();
                                            GameObject objectBuildGO = Instantiate(objectToBeBuild, currentSpawnPoint.transform);
                                            ForceUpdatePath();
                                            objectBuildGO.SetActive(false);
                                            objectToDestroy = objectBuildGO;

                                        }
                                    }
                                }

                            }
                        }
                    }
                    Debug.Log("You selected the " + hit.transform.name);
                }
            }


        }
    }

    private bool UserConfirmsDecision()
    {

        if (InterfaceManager.confirmationHasChanged)
        {
            if (InterfaceManager.confirmationValue)
            {
                return true;
            }
            confirmPanel.SetActive(false);
            InterfaceManager.confirmationHasChanged = false;
            objectToRemove = null;
            return false;
        }

        return false;
    }

    private bool clickedOrTouched()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            return true;
        }

        return false;
    }

    private void DetermineObjectToBuild()
    {
        if (wallToggle.isOn) // wall selected in the UI
        {
            objectToBeBuild = wallPrefab;
        }
        else // Turret selected in the UI
        {
            objectToBeBuild = turretPrefab;
        }
    }

    private bool canReachDestination()
    {
        allAgents = GameObject.FindGameObjectsWithTag("NavAgent");
        foreach (var agent in allAgents)
        {
            var agentControl = agent.GetComponent<AgentControl>();
            if (!agentControl.CalculateNewPath())
            {
                return false;
            }
        }

        return true;
    }

    private void ForceUpdatePath()
    {
        GameObject[] allCurrentAgents = GameObject.FindGameObjectsWithTag("NavAgent");
        foreach (var agent in allCurrentAgents)
        {
            var agentControl = agent.GetComponent<NavMeshAgent>();
            var auxDestination = agentControl.destination;
            agentControl.SetDestination(auxDestination);

        }
    }

}
