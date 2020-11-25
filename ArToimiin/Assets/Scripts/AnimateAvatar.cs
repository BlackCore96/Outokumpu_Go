using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAvatar : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAnimationStateChange(MoveAvatar.AvatarAnimationState animationState)
    {

        switch (animationState)
        {

            case MoveAvatar.AvatarAnimationState.Walk:
            case MoveAvatar.AvatarAnimationState.Run:
                animator.SetBool("isIdle", false);
                break;

            case MoveAvatar.AvatarAnimationState.Idle:
                animator.SetBool("isIdle", true);
                break;

        }
    }
}
