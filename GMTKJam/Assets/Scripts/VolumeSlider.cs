using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private float StartingVolume;
    [SerializeField] private AudioMixer MasterVolume;
    [SerializeField] private Slider Slider;
    [SerializeField] private string VolumeParamater;
    private AudioSource Source;


    private void Start()
    {
        float vol = PlayerPrefs.GetFloat("Volume", StartingVolume);
        Slider.value = vol;
        MasterVolume.SetFloat(VolumeParamater, Mathf.Log10(vol) * 30f);
        Slider.onValueChanged.AddListener(ValueChanged);
    }
    private void ValueChanged(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        MasterVolume.SetFloat(VolumeParamater, Mathf.Log10(value) * 30f);
    }
}
