using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    //Kun haluat pyörittää ääntä, kutsu "AudioManagerScript.instance.PlaySound(AudioManagerScript.SoundClip.*PEIKKO_SWING*)"
    public static AudioManagerScript instanse;

    AudioSource audioSource;
    //Tee tänne uusille äänille oma group!
    public AudioClip[] peikkoSwing;
    public AudioClip[] peikkoDamage;
    public AudioClip[] peikkoDeath;
    public AudioClip[] peikkoRoar;

    //Tee tänne uusi tunnus!
    public enum SoundClip
    {
        PEIKKO_SWING,
        PEIKKO_DEATH,
        PEIKKO_DAMAGE
    }

    AudioClip GetAudioClip(SoundClip clip)
    {
        var a = new AudioClip[0];
        switch (clip)
        {
            //Tee tänne uusi case (ennen default casea)!
            case SoundClip.PEIKKO_SWING:
                a = peikkoSwing;
                break;
            case SoundClip.PEIKKO_DEATH:
                a = peikkoDeath;
                break;
            case SoundClip.PEIKKO_DAMAGE:
                a = peikkoDamage;
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
