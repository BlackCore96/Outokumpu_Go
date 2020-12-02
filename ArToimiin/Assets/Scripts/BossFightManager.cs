using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    AudioManagerScript audioManager;
    AnimatorScript animationManager;

    List<Touch> touches = new List<Touch>();
    public float waitTime;

    private void Start()
    {
        audioManager = GetComponent<AudioManagerScript>();
        animationManager = GetComponent<AnimatorScript>();
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    var t = touch;
                    touches.Add(t);
                    break;
                case TouchPhase.Ended:
                    CheckForFinger(touch);
                    touches.Remove(touch);
                    break;
            }
        }
    }

    void CheckForFinger(Touch touch)
    {
        foreach (Touch t in touches)
        {
            if (t.fingerId.Equals(touch.fingerId))
            {
                CheckForSwipe(t, touch);
            }
        }
    }

    void CheckForSwipe(Touch start, Touch end)
    {
        Vector2 swipeDirection = end.position - start.position;
        if (Mathf.Abs(swipeDirection.x) - Mathf.Abs(swipeDirection.y) > 20)
        {
            if (swipeDirection.x >= 0)
            {
                //right
            }
            else
            {
                //left
            }
        }
    }

    void Swipe()
    {

    }

    IEnumerator PeikkoRandom()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("PeikkoRandom");
    }
}
