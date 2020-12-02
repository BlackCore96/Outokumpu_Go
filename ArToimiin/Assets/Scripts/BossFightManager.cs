using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    AudioManagerScript audioManager;
    AnimatorScript animationManager;

    public static BossFightManager instance;

    List<Touch> touches = new List<Touch>();
    public float waitTime = 2;
    public float reactionTime = 1.5f;
    [HideInInspector]
    bool canSwipe = true;
    public PeikkoState peikkoState;
    public PeikkoState dodgeCommand;
    bool correctCommand;

    private void Start()
    {
        audioManager = GetComponent<AudioManagerScript>();
        animationManager = GetComponent<AnimatorScript>();
        instance = this;
        peikkoState = PeikkoState.DEFAULT;
        dodgeCommand = PeikkoState.DEFAULT;
    }

    public enum PeikkoState
    {
        ATTACK,
        LEFT,
        RIGHT,
        DEFAULT
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
                        touches.Add(touch);
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
        Touch remove = new Touch();
        foreach (Touch t in touches)
        {
            if (t.fingerId.Equals(touch.fingerId))
            {
                CheckForSwipe(t, touch);
                remove = t;
            }
        }
        touches.Remove(remove);
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
                    dodgeCommand = PeikkoState.RIGHT;
                    animationManager.PlayAnimation(AnimatorScript.HeroAnimation.DODGE_RIGHT);
                    break;
                case "left":
                    dodgeCommand = PeikkoState.LEFT;
                    animationManager.PlayAnimation(AnimatorScript.HeroAnimation.DODGE_LEFT);
                    break;
            }
            StartCoroutine("SetHeroDefault");
        }
    }

    IEnumerator SetHeroDefault()
    {
        yield return new WaitForSeconds(waitTime);
        dodgeCommand = PeikkoState.DEFAULT;
    }

    void PlayAnimation(AnimatorScript.BossAnimation animation)
    {
        animationManager.PlayAnimation(animation);
    }

    void PlayAnimation(AnimatorScript.HeroAnimation animation)
    {
        animationManager.PlayAnimation(animation);
    }

    IEnumerator ResolveCommand()
    {
        yield return new WaitForSeconds(reactionTime);
        if (peikkoState.Equals(PeikkoState.ATTACK))
        {
            if (peikkoState == dodgeCommand)
            {
                //tee damagee
                Debug.Log("tee damagee");
            }
        }
        else
        {
            if (peikkoState != dodgeCommand)
            {
                //ota damagee
                Debug.Log("ota damagee");
            }
        }
        peikkoState = PeikkoState.DEFAULT;
    }

    void ChangePeikkoState()
    {
        GetRandomPeikkoState();
    }

    void GetRandomPeikkoState()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                PlayAnimation(AnimatorScript.BossAnimation.ATTACK);
                peikkoState = PeikkoState.ATTACK;
                break;
            case 1:
                PlayAnimation(AnimatorScript.BossAnimation.HIT_LEFT);
                peikkoState = PeikkoState.LEFT;
                break;
            case 2:
                PlayAnimation(AnimatorScript.BossAnimation.HIT_RIGHT);
                peikkoState = PeikkoState.RIGHT;
                break;
        }
    }

    public IEnumerator PeikkoRandom()
    {
        yield return new WaitForSeconds(waitTime);
        if (peikkoState.Equals(PeikkoState.DEFAULT))
        {
            ChangePeikkoState();
        }
        StartCoroutine("ResolveCommand");
        StartCoroutine("PeikkoRandom");
    }
}
