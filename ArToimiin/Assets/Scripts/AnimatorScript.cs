using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem catchParticle;
    public ParticleSystem dustTrailParticle;

    public enum BossAnimation
    {
        ATTACK = 0,
        HIT_LEFT = 1,
        HIT_RIGHT = 2,
        DAMAGE = 3,
        ROAR = 4,
        DEATH = 5
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

    public void PlayBossAnimation(BossAnimation animation)
    {
        string s = Animation(animation);
        animator.SetTrigger(s);
    }
}
