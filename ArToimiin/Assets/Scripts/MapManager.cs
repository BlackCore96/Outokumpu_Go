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

    public Sprite[] unlockedImages;
    public Sprite[] lockedImages;
    public Image[] libraryCharacters;

    public Slider progressMeter;
    public Text progressPercentage;
    public Transform characterLibraryTransform;
    public Scrollbar scrollbar;

    private float multiplier = 100;
    private float libraryStartHeight;

    static public bool mapTutorialDone;
    static public bool minigameTutorialDone;
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
        libraryStartHeight = characterLibraryTransform.position.y;
    }

    void LateStart()
    {
        if (win) //asettaa juuri voitetun stopin voitettujen listaan
        {
            foreach (SpawnWorldObjects.POIInfo pOI in SpawnWorldObjects.pOIInfos)
            {
                Debug.Log(pOI.stopID.ToString());
                if (pOI.stopID.Equals(stopID))
                {
                    pOI.isBeaten = true;
                    win = false;
                    break;
                }
            }
        }
        progressMeter.maxValue = SpawnWorldObjects.pOIInfos.Count;
        progressMeter.value = progressMeter.maxValue;
        foreach (SpawnWorldObjects.POIInfo pOI in SpawnWorldObjects.pOIInfos)
        {
            if (pOI.isBeaten)
            {
                progressMeter.value--;
            }
        }
        progressPercentage.text = (100 -(progressMeter.value / progressMeter.maxValue * 100)).ToString() + "%";

        if (!mapTutorialDone)
        {
            mapTutorialDone = true;
            HelpperiScript.instanse.StartTutorial(HelpperiScript.HelperText.FIRST_LAUNCH);
        }

        minigameTutorialDone = false;
        for (int i = 0; i < libraryCharacters.Length; i++)
        {
            if (SpawnWorldObjects.pOIInfos[i].isBeaten)
            {
                libraryCharacters[i].sprite = unlockedImages[i];
                minigameTutorialDone = true;
            }
            else
            {
                libraryCharacters[i].sprite = lockedImages[i];
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

    public void LibrarySlider()
    {
        Vector3 newPos = characterLibraryTransform.position;
        newPos.y = libraryStartHeight + (scrollbar.value * multiplier);
        characterLibraryTransform.position = newPos;
    }
}
