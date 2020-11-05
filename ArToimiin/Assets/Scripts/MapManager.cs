using System.Collections;
using System.Collections.Generic;
using GoShared;
using GoMap;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum TriggerState
    {
        Enter,
        Exit
    }

    public List<GameObject> pOIs = new List<GameObject>();

    public void Trigger(Collider collider, TriggerState state)
    {
        if (collider.CompareTag("POI"))
        {

        }
    }
}
