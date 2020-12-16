using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HelpperiScript : MonoBehaviour
{
    public static HelpperiScript instanse;
    CanvasGroup canvasGroup;
    Text text;
    Image image;
    public Sprite[] sprites;
    public float typeSpeed;
    public float sentenceEnd;
    public float space;

    public bool done;

    float tempTypeSpeed;
    float tempSentenceEnd;
    float tempSpace;

    [Header("Helpperi tekstit")]
    public string[] allCharactersCollected;
    public string[] firstLaunch;
    public string[] firstMinigame;
    public string[] error;

    bool isWriting;
    bool touched;
    bool isJuggling;

    int i;

    public enum HelperText
    {
        FIRST_LAUNCH,
        FIRST_MINIGAME,
        ALL_CHARACTERS_COLLECTED
    }

    public string[] GetHelperText(HelperText helper)
    {
        switch (helper)
        {
            case HelperText.ALL_CHARACTERS_COLLECTED:
                return allCharactersCollected;
            case HelperText.FIRST_LAUNCH:
                return firstLaunch;
            case HelperText.FIRST_MINIGAME:
                return firstMinigame;
            default:
                return error;
        }
    }

    void Start()
    {
        instanse = this;
        done = false;
        isJuggling = false;
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponentInChildren<Text>();
        image = gameObject.transform.GetChild(1).GetComponent<Image>();
    }

    void ToggleCanvasGroup()
    {
        canvasGroup.alpha = Mathf.Abs(canvasGroup.alpha - 1);
        canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
        if (canvasGroup.alpha.Equals(0))
        {
            AudioManagerScript.instanse.PlaySound(AudioManagerScript.SoundClip.PUHEKUPLA_AUKI);
        }
        else
        {
            AudioManagerScript.instanse.PlaySound(AudioManagerScript.SoundClip.PUHEKUPLA_KIINNI);
        }
    }

    IEnumerator juggleSprites()
    {
        image.sprite = sprites[i];
        yield return new WaitForSeconds(.15f);
        i++;
        if (i.Equals(4))
        {
            i = 0;
        }
        if (isJuggling)
        {
            StartCoroutine("juggleSprites");
        }
    }

    private void Update()
    {
        if (!Application.isEditor)
        {
            if (!Input.touchCount.Equals(0))
            {
                tempTypeSpeed = 0;
                tempSentenceEnd = 0;
                tempSpace = 0;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            tempTypeSpeed = 0;
            tempSentenceEnd = 0;
            tempSpace = 0;
        }
    }

    public void StartTutorial(HelperText tutorial)
    {
        StartCoroutine("ShowTutorial", tutorial);
    }

    IEnumerator ShowTutorial(HelperText tutorial)
    {
        ToggleCanvasGroup();
        string[] vs = GetHelperText(tutorial);
        foreach (string s in vs)
        {
            tempTypeSpeed = typeSpeed;
            tempSentenceEnd = sentenceEnd;
            tempSpace = space;
            isJuggling = true;
            i = 0;
            StartCoroutine("juggleSprites");
            StartCoroutine("WriteText", s);
            yield return new WaitUntil(() => !isWriting);
            isJuggling = false;
            if (!Application.isEditor)
            {
                yield return new WaitUntil(() => !Input.touchCount.Equals(0));
            }
            else
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
        }
        ToggleCanvasGroup();
        done = true;
    }

    IEnumerator WriteText(string s)
    {
        isWriting = true;
        char[] characters = s.ToCharArray();
        text.text = "";
        foreach (char character in characters)
        {
            text.text = text.text.Insert(text.text.Length, character.ToString());
            float waitTime;
            switch (character.ToString())
            {
                case "!":
                    waitTime = tempSentenceEnd;
                    break;
                case "?":
                    waitTime = tempSentenceEnd;
                    break;
                case ".":
                    waitTime = tempSentenceEnd;
                    break;
                case " ":
                    waitTime = tempSpace;
                    break;
                default:
                    waitTime = tempTypeSpeed;
                    break;
            }
            yield return new WaitForSeconds(waitTime);
        }
        isWriting = false;
    }
}
