using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }
    
    public void PlaySoundOnce(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    
    public void PlaySoundOnce2(AudioClip clip)
    {
        PlaySound(clip);
        Invoke("StopSound", 1f);
    }
    
    public void PlaySound(AudioClip clip)
    {
        if (!source.isPlaying)
        {
            source.clip = clip;
            source.Play();
        }
    }

    public void StopSound()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
    }
}
