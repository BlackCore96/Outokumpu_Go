using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HelpperiScript : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Text text;
    public float typeSpeed;

    [Header("Helpperi tekstit")]
    public string[] allCharactersCollected;
    public string[] firstLaunch;
    public string[] firstMinigame;
    public string[] error;

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
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponentInChildren<Text>();
    }

    IEnumerator WriteText(string s)
    {
        text.text = "";
        int i = 0;
        foreach (char character in s)
        {
            text.text.Insert(i, character.ToString());
            i++;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
