using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instanse;

    AudioSource audioSource;
    public AudioClip[] peikkoSwing;
    public AudioClip[] peikkoDeath;
    public AudioClip[] peikkoRoar;

    public enum SoundClip
    {
        PEIKKO_SWING = 0,
        PEIKKO_DEATH = 1
    }

    AudioClip GetAudioClip(SoundClip clip)
    {
        var a = new AudioClip[0];
        switch (clip)
        {
            case SoundClip.PEIKKO_SWING:
                a = peikkoSwing;
                break;
            case SoundClip.PEIKKO_DEATH:
                a = peikkoDeath;
                break;
            default:
                a = peikkoRoar;
                break;
        }
        return a[Random.Range(0, a.Length)];
    }

    private void Start()
    {
        instanse = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundClip clip)
    {
        audioSource.PlayOneShot(GetAudioClip(clip));
    }
}
