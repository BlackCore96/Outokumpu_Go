using System.Collections;
using System.Collections.Generic;
using GoShared;
using GoMap;
using System.Linq;
using UnityEngine;

public class SpawnWorldObjects : MonoBehaviour
{
    public GameObject stopPrefab;
    Coordinates coordinates = new Coordinates(62.725284000000, 29.018918000000, 0);

    private void Start()
    {
        GameObject stop = Instantiate(stopPrefab);
        Vector3 position = coordinates.convertCoordinateToVector(0);
        Debug.Log(Coordinates.convertVectorToCoordinates(position).latitude.ToString() + " / " + Coordinates.convertVectorToCoordinates(position).longitude.ToString());
        Debug.Log(coordinates.latitude.ToString() + " / " + coordinates.longitude.ToString());
        Debug.Log(position.x.ToString() + " / " + position.z.ToString());
        stop.transform.localPosition = position;
    }
}
