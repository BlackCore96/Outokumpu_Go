using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UccoMovement : MonoBehaviour
{
    public float movementSpeed;
    private NavMeshSurface navMeshSurface;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination();
    }

    private void Update()
    {
        if (!navMeshAgent.hasPath)
        {
            SetDestination();
        }
    }

    void SetDestination()
    {
        navMeshAgent.SetDestination(navMeshSurface.transform.position + (Vector3.forward * Random.Range(-4f, 4f)) + (Vector3.right * Random.Range(-4f, 4f)));
    }

}
