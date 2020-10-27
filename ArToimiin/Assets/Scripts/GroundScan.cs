using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
using System;

public class GroundScan : MonoBehaviour
{
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRayCastManager;
    Vector3 screenCenter;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRayCastManager = FindObjectOfType<ARRaycastManager>();
        screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(.5f, .5f));
    }

    void ScanGround()
    {
        arRayCastManager.Raycast(screenCenter, hits, TrackableType.Planes);
    }
}
