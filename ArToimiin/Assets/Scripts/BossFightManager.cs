using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    AudioManagerScript audioManager;
    AnimatorScript animationManager;

    public static BossFightManager instance;

    List<Touch> touches = new List<Touch>();
    public float waitTime = 2;
    public float reactionTime = 1.5f;
    bool canSwipe = true;
    public Image attackButton;

    public PeikkoState peikkoState;
    public PeikkoState dodgeCommand;
    bool correctCommand;
    int peikkoActionCount;

    private void Start()
    {
        audioManager = GetComponent<AudioManagerScript>();
        animationManager = GetComponent<AnimatorScript>();
        instance = this;
        peikkoState = PeikkoState.DEFAULT;
        dodgeCommand = PeikkoState.DEFAULT;
        peikkoActionCount = 0;
        Color color = attackButton.color;
        color.a = 0;
        attackButton.color = color;
        attackButton.transform.GetComponent<Button>().interactable = false;
    }

    public enum PeikkoState
    {
        ATTACK,
        LEFT,
        RIGHT,
        DEFAULT,
        VULNERABLE
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
        if (Mathf.Abs(swipeDirection.x) - Mathf.Abs(swipeDirection.y) >= Screen.width / 6)
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

    public void AttackButton()
    {
        Swipe("Attack");
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
                case "Attack":
                    dodgeCommand = PeikkoState.ATTACK;
                    animationManager.PlayAnimation(AnimatorScript.HeroAnimation.ATTACK);
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

    void ResolveCommand()
    {
        if (peikkoActionCount.Equals(0))
        {
            Color color = attackButton.color;
            color.a = 0;
            attackButton.color = color;
            attackButton.transform.GetComponent<Button>().interactable = false;
        }
        
        switch (peikkoState)
        {
            case PeikkoState.ATTACK:
                if (!dodgeCommand.Equals(PeikkoState.LEFT) && !dodgeCommand.Equals(PeikkoState.RIGHT))
                {
                    TakeDamage();
                }
                return;
            case PeikkoState.RIGHT:
                if (!dodgeCommand.Equals(PeikkoState.RIGHT))
                {
                    TakeDamage();
                }
                return;
            case PeikkoState.LEFT:
                if (!dodgeCommand.Equals(PeikkoState.LEFT))
                {
                    TakeDamage();
                }
                return;
            case PeikkoState.VULNERABLE:
                if (dodgeCommand.Equals(PeikkoState.ATTACK))
                {
                    //tee damagee
                }
                return;
        }
        peikkoState = PeikkoState.DEFAULT;
    }

    void ChangePeikkoState()
    {
        peikkoActionCount++;
        if (peikkoActionCount.Equals(5))
        {
            peikkoActionCount = 0;
            PlayAnimation(AnimatorScript.BossAnimation.VULNERABLE);
            peikkoState = PeikkoState.VULNERABLE;
            Color color = attackButton.color;
            color.a = 1;
            attackButton.color = color;
            attackButton.transform.GetComponent<Button>().interactable = true;
            return;
        }
        switch (Random.Range(0, 3))
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

    void TakeDamage()
    {
        Debug.Log("Take Damage");
    }

    public IEnumerator PeikkoRandom()
    {
        ChangePeikkoState();
        yield return new WaitForSeconds(reactionTime);
        ResolveCommand();
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("PeikkoRandom");
    }
}
