using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityScript.Steps;
using System;

public class enemyControllMap2 : MonoBehaviour
{
    public static bool canStartGame = false;

    public float delaytime;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject nagaent;
    public GameObject goalObject;
    public TextMeshProUGUI CounterUI;
    public TextMeshProUGUI MesageUI;
    public float countdown = 6f;

    public List<int> enemiesPerWave;
    public List<float> enemySpeedPerWave;

    private int currentWave;

    private CurrencyLogic currencyLogic;

    private float timer;
    private bool isMapLoaded;

    

    private bool canSpawnNextWave = true;
    private bool canCheckForNextWave = false;
    private float countdownInitialVallue;
    private bool firstWaveSpawned = false;


    private GameObject spawnPoint;


    private void Start()
    {
        currencyLogic = CurrencyLogic.getinstance();
        isMapLoaded = DefaultTrackableEventHandler.isMapLoaded;
        currentWave = 0;
        countdownInitialVallue = countdown;
        CounterUI.text = filterFloat(countdown).ToString() + " sec";

    }

    int filterFloat(float number)
    {
        int seconds;
        var arr = number.ToString().Split('.');
        seconds = Int32.Parse(arr[0]);
        return seconds;
    }

    public void FixedUpdate()
    {
        isMapLoaded = DefaultTrackableEventHandler.isMapLoaded;

        if (!canSpawnNextWave)
        {
           
            CounterUI.text = filterFloat(countdownInitialVallue).ToString() + " sec";
        }
        else
        {
            CounterUI.text = filterFloat(countdown).ToString() + " sec";
        }

        if (!firstWaveSpawned)
        {
            //AgentControl.canReachTarget = true;
            countdown -= Time.deltaTime;
            if (countdown <= 0f)
            {
                firstWaveSpawned = true;
                countdown = countdownInitialVallue;
                StartCoroutine(SpawnWave());
                canSpawnNextWave = false;
            }
        }
        else
        {
            if (currencyLogic.GetAlives() <= 0 && canCheckForNextWave)
            {
                canSpawnNextWave = true;
                canCheckForNextWave = false;
                countdown = countdownInitialVallue;
            }

            if (!canSpawnNextWave)
            {
                AgentControl.canReachTarget = true;
            }

            countdown -= Time.deltaTime;
           
            if (countdown <= 0f && canSpawnNextWave)
            {
                canSpawnNextWave = false;

                if (currentWave < enemiesPerWave.Count)
                {
                    StartCoroutine(SpawnWave());
                }

            }
        }

    }

    IEnumerator SpawnWave()
    {
        var numberOfEnemies = enemiesPerWave[currentWave];
        var enemySpeed = enemySpeedPerWave[currentWave];
        canCheckForNextWave = false;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnAgent();
            yield return new WaitForSeconds(0.7f);
        }
        canCheckForNextWave = true;


        currentWave++;
    }

    void SpawnAgent()
    {

        if (currentWave == 0)
        {
            spawnPoint = spawnPoint1;
        }
        else
        {
            var randomNumber = UnityEngine.Random.Range(1, 10);
            if (randomNumber % 2 == 0)
            {
                spawnPoint = spawnPoint1;
            }
            else
            {
                spawnPoint = spawnPoint2;
            }
        }

      
        currencyLogic.AddAlive();
        currencyLogic.UpdateCurrencyOnDisplay();
        GameObject agentGO = Instantiate(nagaent, spawnPoint.transform.position, Quaternion.identity);
        //NavMeshAgent navMeshAgent = agentGO.GetComponent<NavMeshAgent>();
        //if (navMeshAgent.enabled && !navMeshAgent.isOnNavMesh)
        //{
        //    var position = transform.position;
        //    NavMeshHit hit;
        //    NavMesh.SamplePosition(position, out hit, 10.0f, 1);
        //    position = hit.position; // usually this barely changes, if at all
        //    navMeshAgent.Warp(position);
        //}

    }
}
