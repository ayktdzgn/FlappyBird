using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour
{
    public static SoundPool Instance;
    public AudioSource source1;
    public AudioSource source2;
    public AudioSource source3;
    public List<AudioClip> clips = new List<AudioClip>();

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

        if (source1 == null) source1 = gameObject.AddComponent<AudioSource>();
        if (source2 == null) source2 = gameObject.AddComponent<AudioSource>();
        if (source3 == null) source3 = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        ReloadSounds();
    }

    void ReloadSounds()
    {
        clips.Clear();
        // get all valid files
        Object[] obj = Resources.LoadAll("Sounds");
        foreach (var item in obj)
        {
            clips.Add(item as AudioClip);
            //source.outputAudioMixerGroup = AudioMixer.

        }

    }
}
