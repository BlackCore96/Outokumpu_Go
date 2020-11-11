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
