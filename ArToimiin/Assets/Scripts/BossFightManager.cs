using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    AudioManagerScript audioManager;
    AnimatorScript animationManager;

    List<Touch> touches = new List<Touch>();
    public float waitTime;
    public bool canSwipe;

    private void Start()
    {
        audioManager = GetComponent<AudioManagerScript>();
        animationManager = GetComponent<AnimatorScript>();
    }

    private void Update()
    {
        if (canSwipe)
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
                        break;
                }
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
                touches.Remove(t);
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
                Swipe("right");
            }
            else
            {
                Swipe("left");
            }
        }
    }

    void Swipe(string direction)
    {
        if (animationManager.heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleHero"))
        {
            switch (direction)
            {
                case "right":
                    animationManager.PlayAnimation(AnimatorScript.HeroAnimation.DODGE_RIGHT);
                    break;
                case "left":
                    animationManager.PlayAnimation(AnimatorScript.HeroAnimation.DODGE_LEFT);
                    break;
            }
        }
        
    }

    IEnumerator PeikkoRandom()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("PeikkoRandom");
    }
}
