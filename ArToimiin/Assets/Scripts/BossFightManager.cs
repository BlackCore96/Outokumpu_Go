using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    AudioManagerScript audioManager;
    AnimatorScript animationManager;

    public Button resetButton;
    AudioSource music;

    int peikkoDamage;
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
        peikkoDamage = 0;
        audioManager = GetComponent<AudioManagerScript>();
        animationManager = GetComponent<AnimatorScript>();
        music = Camera.main.GetComponent<AudioSource>();
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
                    audioManager.PlaySound(AudioManagerScript.SoundClip.HERO_SWIPE);
                    break;
                case "left":
                    dodgeCommand = PeikkoState.LEFT;
                    animationManager.PlayAnimation(AnimatorScript.HeroAnimation.DODGE_LEFT);
                    audioManager.PlaySound(AudioManagerScript.SoundClip.HERO_SWIPE);
                    break;
                case "Attack":
                    dodgeCommand = PeikkoState.ATTACK;
                    animationManager.PlayAnimation(AnimatorScript.HeroAnimation.ATTACK);
                    audioManager.PlaySound(AudioManagerScript.SoundClip.HERO_SWIPE);
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
                audioManager.PlaySound(AudioManagerScript.SoundClip.PEIKKO_SMASH);
                if (!dodgeCommand.Equals(PeikkoState.LEFT) && !dodgeCommand.Equals(PeikkoState.RIGHT))
                {
                    TakeDamage();
                }
                return;
            case PeikkoState.RIGHT:
                audioManager.PlaySound(AudioManagerScript.SoundClip.PEIKKO_SWING);
                if (!dodgeCommand.Equals(PeikkoState.RIGHT))
                {
                    TakeDamage();
                }
                return;
            case PeikkoState.LEFT:
                audioManager.PlaySound(AudioManagerScript.SoundClip.PEIKKO_SWING);
                if (!dodgeCommand.Equals(PeikkoState.LEFT))
                {
                    TakeDamage();
                }
                return;
            case PeikkoState.VULNERABLE:
                if (dodgeCommand.Equals(PeikkoState.ATTACK))
                {
                    DoDamage();
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

    void DoDamage()
    {
        audioManager.PlaySound(AudioManagerScript.SoundClip.PEIKKO_DAMAGE);
        animationManager.animator.SetTrigger("Damage");
        peikkoDamage++;
        if (peikkoDamage >= 3)
        {
            StartCoroutine("Victory");
        }
    }

    IEnumerator Victory()
    {
        StopCoroutine("PeikkoRandom");
        yield return new WaitForSeconds(1.5f);
        animationManager.animator.SetTrigger("Death");
        animationManager.heroAnimator.SetTrigger("Victory");
        audioManager.PlaySound(AudioManagerScript.SoundClip.HERO_WIN);
        audioManager.PlaySound(AudioManagerScript.SoundClip.PEIKKO_DEATH);
        music.Stop();
        yield return new WaitForSeconds(8);
        animationManager.animator.enabled = false;
    }

    void TakeDamage()
    {
        StopCoroutine("PeikkoRandom");
        animationManager.heroAnimator.SetTrigger("Defeat");
        audioManager.PlaySound(AudioManagerScript.SoundClip.HERO_LOSE);
        music.Stop();
        resetButton.GetComponent<Image>().enabled = true;
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
