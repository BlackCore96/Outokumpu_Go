using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CatchScript : MonoBehaviour
{
    [HideInInspector]
    public GameObject hahmo;
    public float angle = 45f;
    public GameObject placeholder;
    public float catchTime;
    public Slider progressSlider;
    private new Camera camera;
    private GameObject cameraObject;
    Vector3 characterVector;
    Vector3 cameraVector;
    float characterAngleDistance;
    float progress;
    bool isDecaying;
    bool isProgressing;

    private void Start()
    {
        camera = Camera.main;
        cameraObject = camera.gameObject;
        placeholder.SetActive(false);
        progress = 0;
        progressSlider.maxValue = catchTime;
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
                isProgressing = true;
                placeholder.SetActive(true);
                StopCoroutine("ProgressBarDecayDelay");
                isDecaying = false;
            }
            else
            {
                isProgressing = false;

                if (!IsInvoking("ProgressBarDecayDelay"))
                {
                    Invoke("ProgressBarDecayDelay", 1f);
                }
                placeholder.SetActive(false);
            }
        }
        catch
        {

        }

        if (isDecaying)
        {
            progress -= Time.deltaTime;
        }
        else if(isProgressing)
        {
            progress += Time.deltaTime;
        }
        Mathf.Clamp(progress, 0, catchTime);
        progressSlider.value = progress;

        if (progress.Equals(catchTime))
        {
            Debug.Log("caught");
        }
    }

    void ProgressBarDecayDelay()
    {
        isDecaying = true;
    }
}
