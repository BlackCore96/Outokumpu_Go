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
    private GameObject meshObject;
    [Space, Header("Size in m^2 until scanning is complete")]
    public float desiredSize;
    [Space]
    public float meshSize;
    [Space]
    public Text debugLogText;
    public GameObject prefabCharacter;
    public GameObject prefabMuna;
    public GameObject ground;

    [Header("Editor ground")]
    public GameObject groundPrefab;
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRayCastManager;
    private MeshFilter mesh;
    private ARPlane meshSurface;
    [HideInInspector]
    public new Camera camera;
    bool navMeshIsActive;
    Vector3 screenCenter;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void Start()
    {
        navMeshIsActive = false;
        prefabCharacter = MapManager.prefab;
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRayCastManager = FindObjectOfType<ARRaycastManager>();
        camera = arOrigin.GetComponentInChildren<Camera>();
        screenCenter = camera.ViewportToScreenPoint(new Vector3(.5f, .5f));
        AnimatorScript animatorScript = GetComponent<AnimatorScript>();

        animatorScript.animator = prefabCharacter.GetComponent<Animator>();
        if (Application.isEditor)
        {
            ground = Instantiate(groundPrefab, new Vector3(0, -1, 1), Quaternion.identity);
            ground.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
        InvokeRepeating("UpdateNavMesh", .5f, .5f);
    }

    void UpdateNavMesh()
    {
        if (!Application.isEditor)
        {
            if (!navMeshIsActive)
            {
                try
                {
                    meshBounds = new Vector3(mesh.mesh.bounds.size.x * meshObject.transform.localScale.x, 1, mesh.mesh.bounds.size.z * meshObject.transform.localScale.z);
                    meshSize = meshBounds.x * meshBounds.z;
                }
                catch
                {
                    Debug.Log("Searching NavMesh...");
                    debugLogText.text = "Searching NavMesh...";
                    meshSurface = FindObjectOfType<ARPlane>();
                    try
                    {
                        meshObject = meshSurface.gameObject;
                        mesh = meshObject.GetComponent<MeshFilter>();
                        Debug.Log("NavMesh Found!");
                        debugLogText.text += "NavMesh Found!";
                    }
                    catch { }
                }

                if (meshSize >= desiredSize)
                {
                    SpawnGround();
                }
            }
        }
        else
        {
            CancelInvoke("UpdateNavMesh");
            SpawnEgg();
        }
    }

    void SpawnGround()
    {
        CancelInvoke("UpdateNavMesh");
        ground = Instantiate(groundPrefab, meshSurface.transform.position, Quaternion.identity);
        ground.GetComponent<NavMeshSurface>().BuildNavMesh();
        SpawnEgg();
    }

    void SpawnEgg()
    {
        Debug.Log("Spawned the egg!");
        debugLogText.text = "Spawned the egg!";
        navMeshIsActive = true;
        GameObject muna = Instantiate(prefabMuna, ground.transform.position, Quaternion.identity);
    }
}
