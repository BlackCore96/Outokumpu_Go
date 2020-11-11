using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [HideInInspector]
    public bool stopGrowing = false;
    [HideInInspector]
    public Transform stopTransform;
    public Vector3 growSizeVector;
    public Vector3 normalSizeVector;
    public float interpolate;

    public class TriggerState
    {
        public const bool Enter = true;
        public const bool Exit = false;
    }

    public Button miniGameButton;
    public List<GameObject> pOIs = new List<GameObject>();

    private void Start()
    {
        miniGameButton.gameObject.SetActive(false);
        stopGrowing = false;
    }

    private void Update()
    {
        try
        {
            if (stopGrowing)
            {
                if (stopTransform.localScale.x < growSizeVector.x)
                {
                    stopTransform.localScale = Vector3.MoveTowards(stopTransform.localScale, growSizeVector, interpolate);
                }
            }
            else if (stopTransform.localScale.x > normalSizeVector.x)
            {
                stopTransform.localScale = Vector3.MoveTowards(stopTransform.localScale, normalSizeVector, interpolate);
            }
        }
        catch {}
    }

    public void Trigger(bool state)
    {
        miniGameButton.gameObject.SetActive(state);
    }

    public void MiniGameOnButtonPress()
    {
        SceneManager.LoadScene(1);
    }
}
