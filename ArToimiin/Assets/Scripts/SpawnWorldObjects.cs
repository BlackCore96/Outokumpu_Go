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
    public List<Coordinates> poiCoordinates;
    [Header("Testing Points of Interest")]
    public List<Coordinates> riveriaTestCoordinates;
    bool useTestingPois;
    Vector3 position;
    GameObject stop;

    private void Start()
    {
        Invoke("LateStart", .5f);
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
        foreach (Coordinates coordinates in poiCoordinates)
        {
            stop = Instantiate(stopPrefab);          
            mapManager.pOIs.Add(stop); //lisää map managerin listaan kyseisen stopin
            position = coordinates.convertCoordinateToVector();
            stop.transform.position = position; //siirtää stopin oikeaan paikkaan
        }
    }
}
