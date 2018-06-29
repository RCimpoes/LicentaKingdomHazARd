using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class enemyControll : MonoBehaviour
{
    public static bool canStartGame = false;

    public float delaytime;
    public GameObject spawnPoint;
    public GameObject nagaent;
    public GameObject goalObject;
    public TextMeshProUGUI CounterUI;
    public TextMeshProUGUI MesageUI;
    public float countdown = 10f;
    public GameObject startPointPathCheck;

    public List<int> enemiesPerWave;
    public List<float> enemySpeedPerWave;
    
    private int currentWave;

    private CurrencyLogic currencyLogic;

    private float timer;
    private bool isMapLoaded;
    private bool timerShouldBeUpdated = true;

    private bool currentlySpawningWave = false;
   
    private float timeBetweenWaves = 20f;

    private bool canSpawnNextWave = true;
    private bool canCheckForNextWave = false;
    private float countdownInitialVallue;



    private void Start()
    {
        currencyLogic = CurrencyLogic.getinstance();
        isMapLoaded = DefaultTrackableEventHandler.isMapLoaded;
        canStartGame = false;
        currentWave = 0;
        countdownInitialVallue = countdown;
        CounterUI.text = filterFloat(countdownInitialVallue).ToString() + " sec";

    }

    public void FixedUpdate()
    {
        isMapLoaded = DefaultTrackableEventHandler.isMapLoaded;

        if (canCheckForNextWave)
        {
            if (currencyLogic.GetAlives() <= 1)
            {
                canSpawnNextWave = true;
                canCheckForNextWave = false;
                countdown = countdownInitialVallue;
            }
        }

        if (canStartGame)
        {
            if (timerShouldBeUpdated)
            {
                timer = Time.time;
                timerShouldBeUpdated = false;
            }

           
            if(canSpawnNextWave && countdown <=0f)
            {
                canSpawnNextWave = false;
                if (currentWave < enemiesPerWave.Count)
                {
                    StartCoroutine(SpawnWave());
                }
            }


            countdown -= Time.deltaTime;
            if (!canSpawnNextWave)
            {
                CounterUI.text = filterFloat(countdownInitialVallue).ToString() + " sec";
            }
            else
            {
               // AgentControl.canReachTarget = true;
                CounterUI.text = filterFloat(countdown).ToString() + " sec";
                
            }
        }
    }

    int filterFloat(float number)
    {
        int seconds;
        var arr = number.ToString().Split('.');
        seconds = System.Int32.Parse(arr[0]);
        return seconds;
    }

    IEnumerator SpawnWave()
    {
        if(currentWave < enemiesPerWave.Count)
        {
            var numberOfEnemies = enemiesPerWave[currentWave];
            var enemySpeed = enemySpeedPerWave[currentWave];
            canCheckForNextWave = false;

            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnAgent();
                yield return new WaitForSeconds(0.5f);
            }
            canCheckForNextWave = true;

            currentWave++;
        }
       
    }

    void SpawnAgent()
    {
        currencyLogic.AddAlive();
        currencyLogic.UpdateCurrencyOnDisplay();
        GameObject agentGO = Instantiate(nagaent, spawnPoint.transform.position, Quaternion.identity);
        NavMeshAgent navMeshAgent = agentGO.GetComponent<NavMeshAgent>();
        if (navMeshAgent.enabled && !navMeshAgent.isOnNavMesh)
        {
            var position = transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(position, out hit, 10.0f, 1);
            position = hit.position; // usually this barely changes, if at all
            navMeshAgent.Warp(position);
        }

    }
}
