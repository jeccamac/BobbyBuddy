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

        musicButton.sprite = musicOn;
        sfxButton.sprite = soundOn;
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();

        if (musicCount == 0)
        {
            musicButton.sprite = musicOff;
            musicCount++;
        } else
        { 
            musicButton.sprite = musicOn;
            musicCount = 0;
        }
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();

        if (soundCount == 0)
        {
            sfxButton.sprite = soundOff;
            soundCount++;
        } else
        {
            sfxButton.sprite = soundOn;
            soundCount = 0;
        }
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
