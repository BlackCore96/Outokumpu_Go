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
    public GameObject prefabKolo;
    public GameObject prefabHero;
    [Space]
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRayCastManager;
    private MeshFilter mesh;
    private GameObject meshSurface;
    [HideInInspector]
    public new Camera camera;
    Vector3 screenCenter;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    AnimatorScript animatorScript;

    GameObject kolo;
    bool removeKolo = false;

    void Start()
    {
        prefabCharacter = MapManager.prefab;
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRayCastManager = FindObjectOfType<ARRaycastManager>();
        camera = arOrigin.GetComponentInChildren<Camera>();
        screenCenter = camera.ViewportToScreenPoint(new Vector3(.5f, .5f));
        animatorScript = GetComponent<AnimatorScript>();

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
        removeKolo = false;
    }

    void UpdateNavMesh()
    {
        if (!Application.isEditor)
        {
            meshSurface = GameObject.FindGameObjectWithTag("Plane");
            try
            {
                mesh = meshSurface.GetComponent<MeshFilter>();
                SpawnGround();
            }
            catch { }
        }
    }

    private void Update()
    {
        if (removeKolo)
        {
            kolo.transform.localScale = kolo.transform.localScale - (Vector3.one * Time.deltaTime * .5f);
            if (kolo.transform.localScale.x < 0)
            {
                removeKolo = false;
                Destroy(kolo);
                BossFightManager.instance.StartCoroutine("PeikkoRandom");
            }
        }
    }

    void SpawnGround()
    {
        CancelInvoke("UpdateNavMesh");
        ground = Instantiate(groundPrefab, meshSurface.transform.position, Quaternion.identity);

        if (isBossFight)
        {
            StartCoroutine("SpawnKolo");
            SpawnHero();
        }
        else
        {
            ground.GetComponent<NavMeshSurface>().BuildNavMesh();
            SpawnEgg();
        }
    }

    IEnumerator SpawnKolo()
    {
        GameObject spawn = ground.transform.GetChild(0).gameObject;
        kolo = Instantiate(prefabKolo, spawn.transform.position, spawn.transform.rotation);
        yield return new WaitForSeconds(4);
        SpawnBoss();
        yield return new WaitForSeconds(4);
        removeKolo = true;
    }

    void SpawnHero()
    {
        GameObject spawn = ground.transform.GetChild(1).gameObject;
        GameObject hero = Instantiate(prefabHero, spawn.transform.position, spawn.transform.rotation);
        AudioManagerScript.instanse.PlaySound(AudioManagerScript.SoundClip.HERO_SPAWN);
        animatorScript.heroAnimator = hero.GetComponent<Animator>();
    }

    void SpawnBoss()
    {
        GameObject spawn = ground.transform.GetChild(0).gameObject;
        GameObject boss = Instantiate(prefabBoss, spawn.transform.position, spawn.transform.rotation);
        AudioManagerScript.instanse.PlaySound(AudioManagerScript.SoundClip.PEIKKO_INTROLAUGH);
        animatorScript.animator = boss.GetComponent<Animator>();
    }

    void SpawnEgg()
    {
        AudioManagerScript.instanse.PlaySound(AudioManagerScript.SoundClip.EGG_SPAWN);
        GameObject muna = Instantiate(prefabMuna, ground.transform.position, Quaternion.identity);
    }
}
