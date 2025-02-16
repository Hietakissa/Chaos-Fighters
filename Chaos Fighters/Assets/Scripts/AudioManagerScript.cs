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

        //DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        } 
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
