﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public CanvasGroup exitGroup;
    public bool isMiniGame;

    private void Start()
    {
        exitGroup.alpha = 0;
        exitGroup.blocksRaycasts = false;
        if (isMiniGame && !MapManager.minigameTutorialDone)
        {
            Invoke("LateStart", .2f);
        }
    }

    void LateStart()
    {
        HelpperiScript.instanse.StartTutorial(HelpperiScript.HelperText.FIRST_MINIGAME);
    }

    public void ExitButton()
    {
        exitGroup.alpha = Mathf.Abs(exitGroup.alpha - 1);
        exitGroup.blocksRaycasts = !exitGroup.blocksRaycasts;
    }

    public void ResetBoss()
    {
        SceneManager.LoadScene(3);
    }

    public void ConfirmExit()
    {
        SceneManager.LoadScene(1);
    }
}
