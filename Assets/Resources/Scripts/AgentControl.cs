using System;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class AgentControl : MonoBehaviour
{
    public float magnitude;
    public static bool canReachTarget;
    public GameObject home;
    public Image healthBar;
    public GameObject healthBarComponent;
    public GameObject ArCamera;
    public float howMuchTillHome;

    public float startHealth = 100;
    private float health;

    private int killScore;
    private int aliveScore;


    NavMeshAgent agent;

    private KilledAliveLogic killedAliveLogic;
    private CurrencyLogic currencyLogic;
    private NavMeshPath navMeshPath;

    void Start()
    {
        navMeshPath = new NavMeshPath();
        home = GameObject.Find(home.transform.name);

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(home.transform.position); 

        currencyLogic = CurrencyLogic.getinstance();

        health = startHealth;

        InvokeRepeating("shakeAgentsUp", 3f, 0.1f);
    }

    void shakeAgentsUp()
    {   
        if (agent.velocity.magnitude < 5.0f)
        {
            Debug.Log("I'm stuck!!!!");
            agent.gameObject.SetActive(false);
            agent.gameObject.SetActive(true);
            agent.ResetPath();
            agent.SetDestination(home.transform.position);

        }
    }

    private void Update()
    {
        ArCamera = GameObject.Find("ARCamera");
        healthBarComponent.transform.LookAt(ArCamera.transform);
        //canReachTarget = CalculateNewPath();
                                            
        magnitude = agent.velocity.magnitude;

        if (GetDistanceBetwen(agent.gameObject, home) < 6f)
        {

            Debug.Log("Made it home");

            Destroy(agent.gameObject);

            currencyLogic.DecreaseAlive();
            currencyLogic.DecreaseLifes();
            currencyLogic.UpdateCurrencyOnDisplay();

        }


        if (agent.hasPath)
        {
            Vector3 toTarget = agent.steeringTarget - this.transform.position;
            float turnAngle = Vector3.Angle(this.transform.forward, toTarget);
            agent.acceleration = turnAngle * agent.speed;
        }


    }

    void SetHome()
    {
        agent.SetDestination(home.transform.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    public bool CalculateNewPath()
    {

        agent.CalculatePath(home.transform.position, navMeshPath);
       
        //Debug.Log("status: " + navMeshPath.status);

        if (navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void Die()
    {
        Destroy(gameObject);
        currencyLogic.AddKill();
        currencyLogic.DecreaseAlive();
        currencyLogic.AddAmountToCurrency(50);
    }

    private float GetDistanceBetwen(GameObject g1, GameObject g2)
    {
        float distance = Vector3.Distance(g1.transform.position, g2.transform.position);
        return distance;
    }


    

}
