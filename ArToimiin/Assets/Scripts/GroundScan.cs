using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
using System;

public class GroundScan : MonoBehaviour
{
    public Text debugLogText;
    public GameObject prefabCharacter;
    public GameObject prefabMuna;
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRayCastManager;
    private NavMeshSurface navMeshSurface;
    [HideInInspector]
    public new Camera camera;
    bool navMeshIsActive;
    Vector3 screenCenter;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void Start()
    {
        navMeshIsActive = false;
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRayCastManager = FindObjectOfType<ARRaycastManager>();
        camera = arOrigin.GetComponentInChildren<Camera>();
        screenCenter = camera.ViewportToScreenPoint(new Vector3(.5f, .5f));
        GetComponent<AnimatorScript>().animator = prefabCharacter.GetComponent<Animator>();
        InvokeRepeating("UpdateNavMesh", .5f, .5f);
        InvokeRepeating("SpawnCharacter", .6f, .5f);
    }

    void UpdateNavMesh()
    {
        try
        {
            navMeshSurface.BuildNavMesh();
        }
        catch
        {
            Debug.Log("Searching NavMesh...");
            debugLogText.text = "Searching NavMesh...";
            navMeshSurface = FindObjectOfType<NavMeshSurface>();
            try
            {
                navMeshSurface.BuildNavMesh();
                Debug.Log("NavMesh Found!");
                debugLogText.text = "NavMesh Found!";
                navMeshIsActive = true;
            }
            catch { }
        }
    }

    void SpawnCharacter()
    {
        if (navMeshIsActive)
        {
            GameObject muna = Instantiate(prefabMuna, navMeshSurface.transform.position, Quaternion.identity);
            CancelInvoke("SpawnCharacter");
        }
    }
}
