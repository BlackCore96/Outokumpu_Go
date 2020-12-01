using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] swing;
    public AudioClip[] death;

    public enum SoundClip
    {
        SWING = 0,
        DEATH = 1
    }

    AudioClip GetAudioClip(SoundClip clip)
    {
        var a = new AudioClip[0];
        switch (clip)
        {
            case SoundClip.SWING:
                a = swing;
                break;
            case SoundClip.DEATH:
                a = death;
                break;
            default:
                a = swing;
                break;
        }
        return a[Random.Range(0, a.Length)];
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundClip clip)
    {
        audioSource.PlayOneShot(GetAudioClip(clip));
    }
}
