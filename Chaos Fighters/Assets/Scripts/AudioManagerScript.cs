<<<<<<< Updated upstream
=======
using System;
using System.Collections.Generic;

using Unity.Mathematics;

using UnityEditor.Rendering;
>>>>>>> Stashed changes
using UnityEngine;
using System;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript instance;
    /*

    public Sound[] musicSounds, sounds;
    public AudioSource musicSource, sfxSource;
    */

    public Sound[] sounds;
    public Sound[] hitSounds;
<<<<<<< Updated upstream
    public Sound[] lionPainSounds;
    public Sound[] womanPainSounds;
=======
>>>>>>> Stashed changes

    public float soundVolume = 1;
    public float musicMultiplier = 1;

    void Awake()
    {
 
        if (instance == null) 
        { 
            instance = this; 
        }
        else 
        { 
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        foreach (Sound s in hitSounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
<<<<<<< Updated upstream

        foreach (Sound s in lionPainSounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        foreach (Sound s in womanPainSounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
=======
>>>>>>> Stashed changes
    }

    public void PlayRandomSound(string name)
    {
        if (name == "Hit")
        {
<<<<<<< Updated upstream
            int x = UnityEngine.Random.Range(0, hitSounds.Length);
=======
            int x = UnityEngine.Random.Range(0, sounds.Length);
>>>>>>> Stashed changes
            Sound s = hitSounds[x];
            s.source.volume = soundVolume;
            s.source.Play();
        }
<<<<<<< Updated upstream
        if (name == "LionPain")
        {
            int x = UnityEngine.Random.Range(0, lionPainSounds.Length);
            Sound s = lionPainSounds[x];
            s.source.volume = soundVolume;
            s.source.Play();
        }
        if (name=="WomanPain")
        {
            int x = UnityEngine.Random.Range(0, womanPainSounds.Length);
            Sound s = womanPainSounds[0];
            s.source.volume = soundVolume;
            s.source.Play();
        }
=======
>>>>>>> Stashed changes
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = soundVolume;
        s.source.Play();
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = true;
        s.source.Play();
    }

    public void MusicVolume(float volume)
    {
        string name = "MenuMusic";
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = volume;
    }

    public void SoundVolume(float volume)
    {
        soundVolume = volume;
    }
    
}
