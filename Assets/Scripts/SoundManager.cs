using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    public bool sound = true;

    private void Awake()
    {
        MakeSingleton();
    }
    
    private void MakeSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SoundOnOff()
    {
        sound = !sound;
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        if (sound)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
    
}