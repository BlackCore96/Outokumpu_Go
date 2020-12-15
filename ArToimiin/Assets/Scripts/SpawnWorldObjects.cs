using System.Collections;
using System.Collections.Generic;
using GoShared;
using UnityEngine.UI;
using GoMap;
using System.Linq;
using UnityEngine;

public class SpawnWorldObjects : MonoBehaviour
{
    BaseLocationManager locationManager;
    MapManager mapManager;
    public GameObject stopPrefab;
    public GameObject bossStopPrefab;
    [Header("Points of Interest")]
    public List<POIInfo> poiCoordinates;
    [Header("Testing Points of Interest")]
    public List<POIInfo> riveriaTestCoordinates;

    static public List<POIInfo> pOIInfos;
    public POIInfo bossPOI;
    
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
        Invoke("SpawnStops", 1);
    }

    void SpawnStops()
    {
        int i = 0;
        foreach (POIInfo pOI in pOIInfos)
        {
            if (!pOI.isBeaten)
            {
                i++;
                stop = Instantiate(stopPrefab);
                stop.GetComponent<StopInfoCont>().prefab = mapManager.characters[pOI.stopID];
                stop.GetComponent<StopInfoCont>().stopID = pOI.stopID;
                position = pOI.pOICoordinate.convertCoordinateToVector();
                stop.transform.position = position; //siirtää stopin oikeaan paikkaan
                stop.GetComponent<Animator>().SetFloat("Offset", Random.Range(0f, 1f));
            }
        }
        if (i.Equals(0))
        {
            stop = Instantiate(bossStopPrefab);
            position = bossPOI.pOICoordinate.convertCoordinateToVector();
            stop.transform.position = position; //siirtää stopin oikeaan paikkaan
        }
    }

    [System.Serializable]
    public class POIInfo
    {
        [Tooltip("Ignore altitude")]
        public Coordinates pOICoordinate;
        [Header("Unique ID")]
        public int stopID;
        //[HideInInspector]
        public bool isBeaten;
    }
}
