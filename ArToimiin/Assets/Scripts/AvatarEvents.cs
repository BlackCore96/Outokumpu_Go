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
        Debug.Log("Coll");
        if (other.gameObject.CompareTag("POI"))
        {
            mapManager.Trigger(MapManager.TriggerState.Enter);
            Debug.Log("Enter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("POI"))
        {
            mapManager.Trigger(MapManager.TriggerState.Exit);
            Debug.Log("Exit");
        }
    }
}
