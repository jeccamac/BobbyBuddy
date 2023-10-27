using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /*
    Reference AudioManager from anywhere by using: AudioManager.Instance.PlayMusic("Name") or PlaySFX("Name)
    Stop playing music: AudioManager.Instance.musicSource.Stop();
    */
    public static AudioManager Instance;

    [SerializeField] public Sound[] musicSounds, sfxSounds;
    [SerializeField] public AudioSource musicSource, sfxSource;

    private void Awake() { 
        if (Instance == null) //singleton
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name); //find

        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        } else { Debug.Log("Music Not Found"); }
    }

    public void PlaySFX(string name)
    {
        Sound sound =  Array.Find(sfxSounds, x => x.name == name);
        if (sound != null)
        {
            sfxSource.PlayOneShot(sound.clip);
        }
        else { Debug.Log("SFX Not Found"); }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute; // boolean .mute from AudioSource component
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume; // volume from AudioSource component
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

