using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource firstAudioSource;
    AudioSource secondAudioSource;
    AudioSource thirdAudioSource;
    List<AudioSource>  aa;

    List<AudioClip> sounds;

    public static string flapSound = "flap";
    public static string hitSound = "hit";
    public static string scoreSound = "score";

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        firstAudioSource = SoundPool.Instance.source1;
        secondAudioSource = SoundPool.Instance.source2;
        thirdAudioSource = SoundPool.Instance.source3;
        sounds = SoundPool.Instance.clips;

        aa = new List<AudioSource>();
    }

   public void PlaySoundSource(string sfxName)
    {
        bool flag = false;

        foreach (var audioSource in aa)
        {
            if (!audioSource.isPlaying)
            {
                Instance.PlaySound(sfxName, Instance.sounds, audioSource);
                flag = true;
                return;
            } 
        }
        if (!flag)
        {
            AudioSource ab = gameObject.AddComponent<AudioSource>();
            aa.Add(ab);
            Instance.PlaySound(sfxName, Instance.sounds, ab);
        }
    }

    public void PlaySfxFromFirstAudioSource(string sfxName)
    {
        if (Instance == null)
        {
            Debug.LogWarning("Attempt to play a sound with no SoundManager in the scene");
            return;
        }

        Instance.PlaySound(sfxName, Instance.sounds, Instance.firstAudioSource);
    }

    public void PlaySfxFromSecondAudioSource(string sfxName)
    {
        if (Instance == null)
        {
            Debug.LogWarning("Attempt to play a sound with no SoundManager in the scene");
            return;
        }

        Instance.PlaySound(sfxName, Instance.sounds, Instance.secondAudioSource);
    }

    public void PlaySfxFromThirdAudioSource(string sfxName)
    {
        if (Instance == null)
        {
            Debug.LogWarning("Attempt to play a sound with no SoundManager in the scene");
            return;
        }

        Instance.PlaySound(sfxName, Instance.sounds, Instance.thirdAudioSource);
    }

    private void PlaySound(string soundName, List<AudioClip> pool, AudioSource audioOut)
    {
        // loop through our list of clips until we find the right one.
        foreach (AudioClip clip in pool)
        {
            if (clip.name.Contains(soundName))
            {
                PlaySound(clip, audioOut);
                return;
            }
        }

        Debug.LogWarning("No sound clip found with name " + soundName);
    }

    private void PlaySound(AudioClip clip, AudioSource audioOut)
    {
        audioOut.clip = clip;
        audioOut.Play();
    }
}
