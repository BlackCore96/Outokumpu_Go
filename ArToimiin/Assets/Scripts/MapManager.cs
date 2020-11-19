using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GoShared;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [HideInInspector]
    public bool stopGrowing = false;
    [HideInInspector]
    public Transform stopTransform;
    [Header("MiniPeli ukot")]
    public GameObject[] characters;
    public Vector3 growSizeVector;
    public Vector3 normalSizeVector;
    public float interpolate;
    [Header("MiniGame")]
    [HideInInspector]
    public GameObject characterPrefab;
    static public GameObject prefab;
    static public int stopID;
    static public bool win;

    public class TriggerState
    {
        public const bool Enter = true;
        public const bool Exit = false;
    }

    public Button miniGameButton;

    private void Start()
    {
        miniGameButton.gameObject.SetActive(false);
        stopGrowing = false;
        Invoke("LateStart", .45f);
    }

    void LateStart()
    {
        if (win)
        {
            Debug.Log(stopID.ToString());
            foreach (SpawnWorldObjects.POIInfo pOI in SpawnWorldObjects.pOIInfos)
            {
                Debug.Log(pOI.stopID.ToString());
                if (pOI.stopID.Equals(stopID))
                {
                    Debug.Log("check2");
                    pOI.isBeaten = true;
                    win = false;
                    break;
                }
            }
        }
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
        SceneManager.LoadScene(2);
    }
}
