using System;
using System.Collections.Generic;

using UnityEditor.Rendering;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    void Awake()
    {
        if (instance == null) 
        { 
            instance = this; 
        }
        else 
        { 
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        PlayMusic("MainMenuMusic");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music not found: " + name);
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }

    }
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found: " + name);
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }

    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SoundVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
