using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] public Slider _musicSlider;
    [SerializeField] public Slider _sfxSlider;

    [Header("Buttons")]
    [SerializeField] private Image musicButton;
    [SerializeField] private Image sfxButton;

    [Header("Sprite Settings")]
    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite  soundOff;

    private int musicCount = 0;
    private int soundCount = 0;

    private void Start() 
    {
        _musicSlider.value = AudioManager.Instance.musicSource.volume;
        _sfxSlider.value = AudioManager.Instance.sfxSource.volume;

        UpdateSettings();
        // musicButton.sprite = musicOn;
        // sfxButton.sprite = soundOn;
    }

    private void Update() 
    {
        UpdateSettings();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        AudioManager.Instance.PlaySFX("Button Select");

        if (musicCount == 0)
        {
            //musicButton.sprite = musicOff;
            musicCount++; // soundCount = 1,  turn off
        } else
        { 
            //musicButton.sprite = musicOn;
            musicCount = 0; // soundCount = 0,  turn on
        }
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();

        if (soundCount == 0)
        {
            //sfxButton.sprite = soundOff;
            soundCount++; // soundCount = 1,  turn off
        } else
        {
            //sfxButton.sprite = soundOn;
            soundCount = 0; // soundCount = 0,  turn on
            AudioManager.Instance.PlaySFX("Button Select");
        }
    }

    private void UpdateSettings()
    {
        // in new scene, update sprite status
        if (AudioManager.Instance.musicSource.mute == true ) { musicButton.sprite = musicOff; }
        else { musicButton.sprite = musicOn; }

        if (AudioManager.Instance.sfxSource.mute == true ) { sfxButton.sprite = soundOff; } 
        else { sfxButton.sprite = soundOn; }
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
