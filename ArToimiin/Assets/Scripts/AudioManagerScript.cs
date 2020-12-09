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
    public AudioClip[] peikkoSmash;
    public AudioClip[] peikkoIntroLaugh;
    public AudioClip[] heroSpawn;
    public AudioClip[] heroShoot;
    public AudioClip[] heroWin;
    public AudioClip[] heroLose;
    public AudioClip[] heroSwipe;

    public AudioClip[] catchCreature;
    public AudioClip[] eggSpawn;
    public AudioClip[] eggBreak;
    public AudioClip[] jumpSound;
    public AudioClip[] succesMusic;
    public AudioClip[] succesSound;
    public AudioClip[] piruetti;
    public AudioClip[] granittiRunSound;
    public AudioClip[] rautaRunSound;
    public AudioClip[] scanningSound;
    public AudioClip[] singleStep;

    public AudioClip[] talkingSound;
    public AudioClip[] steps;
    public AudioClip[] catchMeterSound;
    public AudioClip[] puhekuplaAuki;
    public AudioClip[] puhekuplaKiinni;
    public AudioClip[] stepsLoop;
    public AudioClip[] stopOpen;

    //Tee tänne uusi tunnus!
    public enum SoundClip
    {
        PEIKKO_SWING,
        PEIKKO_DEATH,
        PEIKKO_DAMAGE,
        PEIKKO_ROAR,
        PEIKKO_SMASH,
        PEIKKO_INTROLAUGH,
        HERO_SPAWN,
        HERO_SHOOT,
        HERO_WIN,
        HERO_LOSE,
        HERO_SWIPE,
        CATCH_CREATURE,
        EGG_SPAWN,
        EGG_BREAK,
        JUMP_SOUND,
        SUCCES_MUSIC,
        SUCCES_SOUND,
        PIRUETTI,
        GRANIT_RUN,
        RAUTA_RUN,
        SCANNING_SOUND,
        SINGLE_STEP,
        TALKING_SOUND,
        STEPS,
        CATCHMETER_SOUND,
        PUHEKUPLA_AUKI,
        PUHEKUPLA_KIINNI,
        STEPS_LOOP,
        STOP_OPEN
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
            case SoundClip.PEIKKO_ROAR:
                a = peikkoRoar;
                break;
            case SoundClip.PEIKKO_SMASH:
                a = peikkoSmash;
                break;
            case SoundClip.PEIKKO_INTROLAUGH:
                a = peikkoIntroLaugh;
                break;
            case SoundClip.HERO_SPAWN:
                a = heroSpawn;
                break;
            case SoundClip.HERO_SHOOT:
                a = heroShoot;
                break;
            case SoundClip.HERO_WIN:
                a = heroWin;
                break;
            case SoundClip.HERO_LOSE:
                a = heroLose;
                break;
            case SoundClip.HERO_SWIPE:
                a = heroSwipe;
                break;
            case SoundClip.CATCH_CREATURE:
                a = catchCreature;
                break;
            case SoundClip.EGG_SPAWN:
                a = eggSpawn;
                break;
            case SoundClip.EGG_BREAK:
                a = eggBreak;
                break;
            case SoundClip.JUMP_SOUND:
                a = jumpSound;
                break;
            case SoundClip.SUCCES_MUSIC:
                a = succesMusic;
                break;
            case SoundClip.SUCCES_SOUND:
                a = succesSound;
                break;
            case SoundClip.PIRUETTI:
                a = piruetti;
                break;
            case SoundClip.GRANIT_RUN:
                a = granittiRunSound;
                break;
            case SoundClip.RAUTA_RUN:
                a = rautaRunSound;
                break;
            case SoundClip.SCANNING_SOUND:
                a = scanningSound;
                break;
            case SoundClip.SINGLE_STEP:
                a = singleStep;
                break;
            case SoundClip.TALKING_SOUND:
                a = talkingSound;
                break;
            case SoundClip.STEPS:
                a = steps;
                break;
            case SoundClip.CATCHMETER_SOUND:
                a = catchMeterSound;
                break;
            case SoundClip.PUHEKUPLA_AUKI:
                a = puhekuplaAuki;
                break;
            case SoundClip.PUHEKUPLA_KIINNI:
                a = puhekuplaKiinni;
                break;
            case SoundClip.STEPS_LOOP:
                a = stepsLoop;
                break;
            case SoundClip.STOP_OPEN:
                a = stopOpen;
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
