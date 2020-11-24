using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarEvents : MonoBehaviour
{
    MapManager mapManager;

    private void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("POI"))
        {
            mapManager.Trigger(MapManager.TriggerState.Enter);
            mapManager.stopTransform = other.transform;
            mapManager.stopGrowing = true;
            MapManager.stopID = other.GetComponent<StopInfoCont>().stopID;//asettaa kyseisen stopin ID:n muistiin
            MapManager.prefab = other.GetComponent<StopInfoCont>().prefab;//asettaa kyseisen stopin hahmon minipeliin
            Debug.Log("prefab!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("POI"))
        {
            mapManager.Trigger(MapManager.TriggerState.Exit);
            mapManager.stopGrowing = false;
        }
    }
}
