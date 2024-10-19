using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    public void Start() 
    {
        SetVolume(settingsSO.volume);
        volumeSlider.value = settingsSO.volume;

    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        settingsSO.volume = volume;
    }
}
