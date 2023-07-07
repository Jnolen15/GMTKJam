using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public float StartingVolume;
    public AudioMixer MasterVolume;
    public Slider Slider;
    public string VolumeParamater;
    

    private void Awake()
    {
        Slider.onValueChanged.AddListener(ValueChanged);
        ValueChanged(StartingVolume);
    }
    private void ValueChanged(float value)
    {
        MasterVolume.SetFloat(VolumeParamater, Mathf.Log10(value) * 30f);
    }

}
