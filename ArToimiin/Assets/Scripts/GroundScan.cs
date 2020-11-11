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
    public Vector3 meshBounds;
    private GameObject navMeshObject;
    [Space, Header("Size in m^2 until scanning is complete")]
    public float desiredSize;
    [Space]
    public float meshSize;
    [Space]
    public Text debugLogText;
    public GameObject prefabCharacter;
    public GameObject prefabMuna;
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRayCastManager;
    private MeshFilter navMesh;
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
    }

    void UpdateNavMesh()
    {
        if (!navMeshIsActive)
        {
            try
            {
                navMeshSurface.BuildNavMesh();
                meshBounds = new Vector3(navMesh.mesh.bounds.size.x * navMeshObject.transform.localScale.x, 1, navMesh.mesh.bounds.size.z * navMeshObject.transform.localScale.z);
                meshSize = meshBounds.x * meshBounds.z;
            }
            catch
            {
                Debug.Log("Searching NavMesh...");
                debugLogText.text = "Searching NavMesh...";
                navMeshSurface = FindObjectOfType<NavMeshSurface>();
                try
                {
                    navMeshSurface.BuildNavMesh();
                    navMeshObject = navMeshSurface.gameObject;
                    navMesh = navMeshObject.GetComponent<MeshFilter>();
                    Debug.Log("NavMesh Found!");
                    debugLogText.text += "NavMesh Found!";
                }
                catch { }
            }

            if (meshSize >= desiredSize)
            {
                SpawnCharacter();
            }
        }
    }

    void SpawnCharacter()
    {
        Debug.Log("Spawned the egg!");
        debugLogText.text = "Spawned the egg!";
        navMeshIsActive = true;
        GameObject muna = Instantiate(prefabMuna, navMeshSurface.transform.position, Quaternion.identity);
    }
}
