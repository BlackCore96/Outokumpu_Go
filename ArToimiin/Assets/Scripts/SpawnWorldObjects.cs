using System.Collections;
using System.Collections.Generic;
using GoShared;
using GoMap;
using System.Linq;
using UnityEngine;

public class SpawnWorldObjects : MonoBehaviour
{
    public GameObject stopPrefab;
    Coordinates coordinates = new Coordinates(62.732301, 29.054945, 0);
    Vector3 position;
    GameObject stop;

    private void Start()
    {
        Invoke("LateStart", .5f);     
    }

    void LateStart()
    {
        stop = Instantiate(stopPrefab);
        position = coordinates.convertCoordinateToVector();
        stop.transform.position = position;
    }
}
