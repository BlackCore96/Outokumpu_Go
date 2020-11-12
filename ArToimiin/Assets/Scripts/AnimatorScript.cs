using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem catchParticle;
    public ParticleSystem dustTrailParticle;

    public void IsCatched ()
    {
        //catchParticle.Play();
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsStill", false);
        animator.SetTrigger("IsCatched");
    }

    public void IsMoving ()
    {
        animator.SetBool("IsMoving", true);
        animator.SetBool("IsStill", false);
        //dustTrailParticle.Play();
    }

    public void IsStill ()

    {
        animator.SetBool("IsStill", true);
        animator.SetBool("IsMoving", false);
    }
}
