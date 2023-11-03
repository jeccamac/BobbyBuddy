using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] public Slider _musicSlider, _sfxSlider;

    private void Start() 
    {
        _musicSlider.value = AudioManager.Instance.musicSource.volume;
        _sfxSlider.value = AudioManager.Instance.sfxSource.volume;
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
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
