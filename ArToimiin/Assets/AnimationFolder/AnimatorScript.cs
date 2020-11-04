using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsCatched ()
    {
        animator.SetTrigger(0);
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsStill", false);
    }

    public void IsMoving ()
    {
        animator.SetBool("IsMoving", true);
        animator.SetBool("IsStill", false);
    }

    public void IsStill ()

    {
        animator.SetBool("IsStill", true);
        animator.SetBool("IsMoving", false);
    }
}
