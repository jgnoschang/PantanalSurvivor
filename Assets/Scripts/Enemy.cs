using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    public void EnableNavMeshAgent()
    {    
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _navMeshAgent.enabled = true;
        _navMeshAgent.isStopped = false;
    }

    private void LateUpdate()
    {
        if(_navMeshAgent != null && !_navMeshAgent.isStopped)
            _navMeshAgent.destination = player.transform.position;
    }
}
