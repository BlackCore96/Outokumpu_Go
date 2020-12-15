using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAvatar : MonoBehaviour
{
    Animator animator;
    public AudioSource audioSource;
    public AudioClip clip;
    public bool isAudioPlaying;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            isAudioPlaying = true;
        }
        else
        {
            isAudioPlaying = false;
        }
    }

    public void OnAnimationStateChange(MoveAvatar.AvatarAnimationState animationState)
    {

        switch (animationState)
        {

            case MoveAvatar.AvatarAnimationState.Walk:
            case MoveAvatar.AvatarAnimationState.Run:
                if (!isAudioPlaying)
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
