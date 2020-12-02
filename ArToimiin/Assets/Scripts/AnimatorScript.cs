using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    public Animator animator;
    public Animator heroAnimator;
    public ParticleSystem catchParticle;
    public ParticleSystem dustTrailParticle;

    public enum BossAnimation
    {
        ATTACK,
        HIT_LEFT,
        HIT_RIGHT,
        DAMAGE,
        ROAR,
        DEATH
    }

    public enum HeroAnimation
    {
        ATTACK,
        DODGE_LEFT,
        DODGE_RIGHT,
        VICTORY,
        DEFEAT
    }

    public void IsCatched ()
    {
        catchParticle.Play();
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsStill", false);
        animator.SetTrigger("IsCatched");
    }

    public void IsMoving ()
    {
        animator.SetBool("IsMoving", true);
        animator.SetBool("IsStill", false);
        dustTrailParticle.Play();
        Debug.Log("run");
    }

    public void IsStill ()
    {
        animator.SetBool("IsStill", true);
        animator.SetBool("IsMoving", false);
    }

    string Animation(BossAnimation a)
    {
        switch (a)
        {
            case BossAnimation.ATTACK:
                return "Attack";
            case BossAnimation.DAMAGE:
                return "Damage";
            case BossAnimation.DEATH:
                return "Death";
            case BossAnimation.HIT_LEFT:
                return "HitL";
            case BossAnimation.HIT_RIGHT:
                return "HitR";
            case BossAnimation.ROAR:
                return "Roar";
            default:
                return "Roar";
        }
    }

    string Animation(HeroAnimation a)
    {
        switch (a)
        {
            case HeroAnimation.ATTACK:
                return "Attack";
            case HeroAnimation.DEFEAT:
                return "Defeat";
            case HeroAnimation.DODGE_LEFT:
                return "EvadeLeft";
            case HeroAnimation.DODGE_RIGHT:
                return "EvadeRight";
            case HeroAnimation.VICTORY:
                return "Victory";
            default:
                return "Victory";
        }
    }

    public void PlayAnimation(HeroAnimation animation)
    {
        string s = Animation(animation);
        heroAnimator.SetTrigger(s);
    }

    public void PlayAnimation(BossAnimation animation)
    {
        string s = Animation(animation);
        animator.SetTrigger(s);
    }
}
