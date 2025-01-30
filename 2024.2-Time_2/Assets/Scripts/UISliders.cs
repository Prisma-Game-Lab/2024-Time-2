using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliders : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public AudioSource music, sfx; //acho que e inutil no momento mas meio tarde pra tirar e ver no que da

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    private void Start()
    {
        _musicSlider.value = AudioManager.Instance.musicSource.volume;
        _sfxSlider.value = AudioManager.Instance.sfxSource.volume;
    }

    private void Update()
    {
        MusicVolume();
        SFXVolume();
    }
}
