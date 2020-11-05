using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapManager : MonoBehaviour
{
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
