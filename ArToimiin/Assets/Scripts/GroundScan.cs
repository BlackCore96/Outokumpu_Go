using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
using System;

public class GroundScan : MonoBehaviour
{
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRayCastManager;
    private NavMeshSurface navMeshSurface;
    private new Camera camera;
    Vector3 screenCenter;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRayCastManager = FindObjectOfType<ARRaycastManager>();
        try
        {
            navMeshSurface = GameObject.FindGameObjectWithTag("ARPlane").GetComponent<NavMeshSurface>();
        }
        catch
        {
            Invoke("LateStart", .05f);
        }
        camera = arOrigin.GetComponentInChildren<Camera>();
        screenCenter = camera.ViewportToScreenPoint(new Vector3(.5f, .5f));
        InvokeRepeating("UpdateNavMesh", .5f, 3);
    }

    void LateStart()
    {
        navMeshSurface = GameObject.FindGameObjectWithTag("ARPlane").GetComponent<NavMeshSurface>();
    }

    void UpdateNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
