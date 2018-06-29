using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathStatus : MonoBehaviour {

    public GameObject home;
    NavMeshAgent agent;
    NavMeshPath navMeshPath;

    private void Start()
    {
        navMeshPath = new NavMeshPath();
        home = GameObject.Find(home.transform.name);
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(home.transform.position);
    }

    void Update () {

        AgentControl.canReachTarget = CalculateNewPath();
        Debug.Log(AgentControl.canReachTarget);

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
}
