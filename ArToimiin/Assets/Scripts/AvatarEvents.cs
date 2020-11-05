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
        mapManager.Trigger(other, MapManager.TriggerState.Enter);
    }
    private void OnTriggerExit(Collider other)
    {
        mapManager.Trigger(other, MapManager.TriggerState.Exit);

    }
}
