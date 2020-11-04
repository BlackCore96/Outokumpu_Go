using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UccoMovement : MonoBehaviour
{
    [Range(0, 4)]
    public float movementSpeed = 1;
    private NavMeshSurface navMeshSurface;
    private NavMeshAgent navMeshAgent;
    AnimatorScript animatorScript;

    private void Start()
    {
        animatorScript = FindObjectOfType<AnimatorScript>();
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination();
    }

    private void Update()
    {
        navMeshAgent.speed = movementSpeed;
        if (!navMeshAgent.hasPath && !IsInvoking("SetDestination"))
        {

            animatorScript.IsStill();
            Invoke("SetDestination", 3f);
        }
    }

    void SetDestination()
    {
        navMeshAgent.SetDestination(navMeshSurface.transform.position + (Vector3.forward * Random.Range(-4f, 4f)) + (Vector3.right * Random.Range(-4f, 4f)));
        animatorScript.IsMoving();
    }

}
