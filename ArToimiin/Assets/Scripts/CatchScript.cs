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
    public Button leaveMiniGameButton;
    private new Camera camera;
    private GameObject cameraObject;
    Vector3 characterVector;
    Vector3 cameraVector;
    float characterAngleDistance;
    public float progress;
    bool isDecaying;
    bool isProgressing;
    bool isCaught;
    AnimatorScript animatorScript;

    private void Start()
    {
        camera = Camera.main;
        cameraObject = camera.gameObject;
        placeholder.SetActive(false);
        isProgressing = false;
        isDecaying = false;
        isCaught = false;
        progress = 0;
        progressSlider.maxValue = catchTime;
        animatorScript = FindObjectOfType<AnimatorScript>();
        leaveMiniGameButton.enabled = false;
    }

    private void Update()
    {
        try
        {
            characterVector = hahmo.transform.position - cameraObject.transform.position;
            cameraVector = cameraObject.transform.forward;
            characterAngleDistance = Vector3.Angle(cameraVector, characterVector);
            if (!isCaught)
            {
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
                        Invoke("ProgressBarDecayDelay", .3f);
                    }
                    placeholder.SetActive(false);
                }
            }
        }
        catch
        {

        }

        if (isDecaying)
        {
            progress -= Time.deltaTime;
            if (progress <= 0)
            {
                progress = 0;
            }
        }
        else if(isProgressing)
        {
            progress += Time.deltaTime;
            if (progress >= catchTime)
            {
                Caught();
            }
        }
        progressSlider.value = progressSlider.maxValue - progress;
    }

    void Caught()
    {
        hahmo.GetComponent<UccoMovement>().Stop();
        hahmo.GetComponent<UccoMovement>().isCaught = true;
        isProgressing = false;
        isDecaying = false;
        isCaught = true;
        animatorScript.IsCatched();
        leaveMiniGameButton.enabled = true;
        MapManager.win = true;
    }

    void ProgressBarDecayDelay()
    {
        isDecaying = true;
    }

    public void OnButtonPress()
    {
        //animaatioo... muuta asiaa
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
