using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] Slider soundSlider;
    float sliderValue;
    [SerializeField] Toggle fullScreen;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    [Header("SFX")]
    [SerializeField] AudioClip clickSFX;

    AudioSource source;

    public event Action OnSettingsSaved;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (Screen.fullScreen) fullScreen.isOn = true;
        else fullScreen.isOn = false;

        sliderValue = PlayerPrefs.GetFloat("AudioVolume");
        AudioListener.volume = soundSlider.value;

        GetResolutions();
    }

    public void ChangeVolumeSlider(float newValue)
    {
        sliderValue = newValue;
        AudioListener.volume = soundSlider.value;
    }

    public void ToggleFullScreen(bool fullScreen)
    {
        source.PlayOneShot(clickSFX);

        Screen.fullScreen = fullScreen;
    }

    public void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> newOptions = new List<string>();
        int actualResolution = 0;

        for (int i = 0; i < resolutions.Length - 1; ++i)
        {
            newOptions.Add(resolutions[i].width + " x " + resolutions[i].height);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }

        resolutionDropdown.AddOptions(newOptions);
        resolutionDropdown.value = actualResolution;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
    }

    public void ChangeResoution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("AudioVolume", sliderValue);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);

        source.PlayOneShot(clickSFX);

        OnSettingsSaved?.Invoke();
    }
}
