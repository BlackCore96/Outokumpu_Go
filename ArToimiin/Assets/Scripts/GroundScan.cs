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
    [Space, Header("Size in m^2 until scanning is complete")]
    public float desiredSize;
    [Space]
    public float meshSize;
    [Space]
    public GameObject prefabCharacter;
    public GameObject prefabMuna;
    public GameObject ground;
    public GameObject groundPrefab;

    [Header("Editor Spawn Object")]
    public GameObject editorObject;

    [Header("Boss Fight Scene")]
    public bool isBossFight;
    public GameObject prefabBoss;
    [Space]
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRayCastManager;
    private MeshFilter mesh;
    private GameObject meshSurface;
    [HideInInspector]
    public new Camera camera;
    //bool navMeshIsActive;
    Vector3 screenCenter;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void Start()
    {
        //navMeshIsActive = false;
        prefabCharacter = MapManager.prefab;
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRayCastManager = FindObjectOfType<ARRaycastManager>();
        camera = arOrigin.GetComponentInChildren<Camera>();
        screenCenter = camera.ViewportToScreenPoint(new Vector3(.5f, .5f));
        AnimatorScript animatorScript = GetComponent<AnimatorScript>();
        if (isBossFight)
        {
            animatorScript.animator = prefabBoss.GetComponent<Animator>();
        }
        else
        {
            animatorScript.animator = prefabCharacter.GetComponent<Animator>();
        }
        InvokeRepeating("UpdateNavMesh", .5f, .5f);
        if (Application.isEditor)
        {
            meshSurface = Instantiate(editorObject, new Vector3(0, -.5f, 1), Quaternion.identity);
            SpawnGround();
            CancelInvoke("UpdateNavMesh");
        }
        
    }

    void UpdateNavMesh()
    {
        if (!Application.isEditor)
        {
            //if (!navMeshIsActive)
            //{
                //try
                //{
                    //meshBounds = new Vector3(mesh.mesh.bounds.size.x * meshSurface.transform.localScale.x, 1, mesh.mesh.bounds.size.z * meshSurface.transform.localScale.z);
                    //meshSize = meshBounds.x * meshBounds.z;
                //}
                //catch
                //{
                    meshSurface = GameObject.FindGameObjectWithTag("Plane");
                    try
                    {
                        mesh = meshSurface.GetComponent<MeshFilter>();
                        SpawnGround();
                    }
                    catch { }
                //}

                //if (meshSize >= desiredSize)
                //{
                //    SpawnGround();
                //}
            //}
        }
    }

    void SpawnGround()
    {
        CancelInvoke("UpdateNavMesh");
        ground = Instantiate(groundPrefab, meshSurface.transform.position, Quaternion.identity);
        ground.GetComponent<NavMeshSurface>().BuildNavMesh();
        if (isBossFight)
        {
            SpawnBoss();
        }
        else
        {
            SpawnEgg();
        }
    }

    void SpawnBoss()
    {
        GameObject boss = Instantiate(prefabBoss, ground.transform.position, Quaternion.identity);
    }

    void SpawnEgg()
    {
        //navMeshIsActive = true;
        GameObject muna = Instantiate(prefabMuna, ground.transform.position, Quaternion.identity);
    }
}
