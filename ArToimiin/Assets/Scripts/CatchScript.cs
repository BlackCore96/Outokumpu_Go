using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchScript : MonoBehaviour
{
    [HideInInspector]
    public GameObject hahmo;
    public float angle = 45f;
    public GameObject placeholder;
    private new Camera camera;
    private GameObject cameraObject;
    Vector3 characterVector;
    Vector3 cameraVector;
    float characterAngleDistance;
    bool isInNet;

    private void Start()
    {
        camera = Camera.main;
        cameraObject = camera.gameObject;
    }

    private void Update()
    {
        try
        {
            characterVector = hahmo.transform.position - cameraObject.transform.position;
            cameraVector = cameraObject.transform.forward;
            characterAngleDistance = Vector3.Angle(cameraVector, characterVector);
            if (characterAngleDistance <= angle)
            {
                isInNet = true;
                placeholder.SetActive(true);
            }
            else
            {
                isInNet = false;
                placeholder.SetActive(false);
            }
        }
        catch
        {

        }
    }
}
