using System.Collections;
using System.Collections.Generic;
using GoShared;
using GoMap;
using System.Linq;
using UnityEngine;

public class SpawnWorldObjects : MonoBehaviour
{
    BaseLocationManager locationManager;
    MapManager mapManager;
    public GameObject stopPrefab;
    [Header("Points of Interest")]
    public List<POIInfo> poiCoordinates;
    [Header("Testing Points of Interest")]
    public List<POIInfo> riveriaTestCoordinates;

    static public List<POIInfo> pOIInfos;
    bool useTestingPois;
    Vector3 position;
    GameObject stop;

    private void Start()
    {
        Invoke("LateStart", .4f);
    }

    void LateStart()
    {
        locationManager = FindObjectOfType<BaseLocationManager>();
        mapManager = FindObjectOfType<MapManager>();
        useTestingPois = locationManager.useRiveriaOrigin;
        if (useTestingPois)
        {
            poiCoordinates = riveriaTestCoordinates;
        } //testaus stopit
        if (SaveManager.firstLaunch)
        {
            pOIInfos = new List<POIInfo>();
            pOIInfos = poiCoordinates;
        }
        pOIInfos = new List<POIInfo>();
        pOIInfos = poiCoordinates;

        Invoke("SpawnStops", 1);
    }

    void SpawnStops()
    {
        foreach (POIInfo pOI in pOIInfos)
        {
            if (!pOI.isBeaten)
            {
                stop = Instantiate(stopPrefab);
                stop.GetComponent<StopInfoCont>().prefab = pOI.prefabCharacter;
                stop.GetComponent<StopInfoCont>().stopID = pOI.stopID;
                position = pOI.pOICoordinate.convertCoordinateToVector();
                stop.transform.position = position; //siirtää stopin oikeaan paikkaan
                stop.GetComponent<Animator>().SetFloat("Offset", Random.Range(0f, 1f));
            }
        }
    }

    [System.Serializable]
    public class POIInfo
    {
        [Tooltip("Ignore altitude")]
        public Coordinates pOICoordinate;
        [Space]
        public GameObject prefabCharacter;
        [Header("Unique ID")]
        public int stopID;
        //[HideInInspector]
        public bool isBeaten;
    }
}
