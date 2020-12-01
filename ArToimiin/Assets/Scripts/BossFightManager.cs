using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    AudioManagerScript audioManager;
    AnimatorScript animationManager;

    [Header("Testi animaatio toggle napit")]
    public bool swing = false;
    public bool death = false;

    private void Start()
    {
        audioManager = GetComponent<AudioManagerScript>();
        animationManager = GetComponent<AnimatorScript>();
    }

    private void Update()
    {
        if (swing)
        {
            audioManager.PlaySound(AudioManagerScript.SoundClip.SWING);
            animationManager.PlayBossAnimation(AnimatorScript.BossAnimation.HIT_LEFT);
            swing = false;
        }
        if (death)
        {
            audioManager.PlaySound(AudioManagerScript.SoundClip.DEATH);
            animationManager.PlayBossAnimation(AnimatorScript.BossAnimation.DEATH);
            death = false;
        }
    }
}
