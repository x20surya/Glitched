using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXsource;

    [Header("-----Audio Clip------")]

    public AudioClip background;
    public AudioClip bossbattle;



    public AudioClip slimeWalking1;
    public AudioClip slimeWalking2;
    public AudioClip slimeSpit;
    public AudioClip slimePossess;
    public AudioClip slimeLand;
    public AudioClip slimeJump;
    public AudioClip slimeDash;
    public AudioClip slimeGlide;
    public AudioClip slimeWebSwing;

    public AudioClip levelFail;
    public AudioClip doorUnlock;

    public AudioClip knighSwing;
    public AudioClip knightDeath;

    public AudioClip spider;

    public AudioClip dragonDuringRockSlide;
    public AudioClip dragonEruption;
    public AudioClip dragonFlying;
    public AudioClip dragonGrowl;
    public AudioClip flameThrower;
    public AudioClip noPainNoGain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = background;
        musicSource.volume = 0.5f;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        SFXsource.PlayOneShot(clip, volume);
    }
}
