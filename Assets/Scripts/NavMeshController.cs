using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshSurface = gameObject.GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().EnableNavMeshAgent();
        }
    }
}
