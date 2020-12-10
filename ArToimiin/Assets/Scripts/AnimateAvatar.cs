using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAvatar : MonoBehaviour
{
    Animator animator;
    public AudioSource audioSource;
    public AudioClip clip;
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
                audioSource.clip = clip;
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                animator.SetBool("isIdle", false);
                break;

            case MoveAvatar.AvatarAnimationState.Idle:
                audioSource.Stop();
                animator.SetBool("isIdle", true);
                break;

        }
    }
}
