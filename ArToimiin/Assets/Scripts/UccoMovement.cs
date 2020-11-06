using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UccoMovement : MonoBehaviour
{
    [Range(0, 4)]
    public float movementSpeed = 1;
    new Camera camera;
    public bool isCaught;
    GameObject gameManager;
    private NavMeshSurface navMeshSurface;
    private NavMeshAgent navMeshAgent;
    AnimatorScript animatorScript;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        gameManager.GetComponent<AnimatorScript>().animator = GetComponent<Animator>();
        isCaught = false;
        camera = FindObjectOfType<GroundScan>().camera;
    }

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
        if (isCaught)
        {
            transform.LookAt(camera.transform, Vector3.up);
        }
        else
        {
            if (!navMeshAgent.hasPath && !IsInvoking("SetDestination"))
            {
                animatorScript.IsStill();
                Invoke("SetDestination", 1f);
            }
        }
    }

    void SetDestination()
    {
        navMeshAgent.SetDestination(navMeshSurface.transform.position + (Vector3.forward * Random.Range(-4f, 4f)) + (Vector3.right * Random.Range(-4f, 4f)));
        animatorScript.IsMoving();
    }

    public void Stop()
    {
        CancelInvoke();
        navMeshAgent.Stop();
    }

}
