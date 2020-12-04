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
        isJuggling = false;
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponentInChildren<Text>();
        image = gameObject.transform.GetChild(1).GetComponent<Image>();
    }

    void ToggleCanvasGroup()
    {
        canvasGroup.alpha = Mathf.Abs(canvasGroup.alpha - 1);
        canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
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
                    waitTime = sentenceEnd;
                    break;
                case "?":
                    waitTime = sentenceEnd;
                    break;
                case ".":
                    waitTime = sentenceEnd;
                    break;
                case " ":
                    waitTime = space;
                    break;
                default:
                    waitTime = typeSpeed;
                    break;
            }
            yield return new WaitForSeconds(waitTime);
        }
        isWriting = false;
    }
}
