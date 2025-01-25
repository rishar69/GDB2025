using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    public static event Action<float, float> OnSettingChanged;

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;

    private void OnEnable()
    {
        sfxSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        bgmSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    private void OnDisable()
    {
        sfxSlider.onValueChanged.RemoveAllListeners();
        bgmSlider.onValueChanged.RemoveAllListeners();
    }

    private void ChangeVolume()
    {
        float sfxVolume = sfxSlider.value;
        float bgmVolume = bgmSlider.value;

        OnSettingChanged?.Invoke(sfxVolume, bgmVolume);
        SaveSettings(sfxVolume, bgmVolume);
    }

    private void SaveSettings(float sfx, float bgm)
    {
        PlayerPrefs.SetFloat("SfxVolume", sfx);
        PlayerPrefs.SetFloat("BgmVolume", bgm);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        float bgmVolume = PlayerPrefs.GetFloat("BgmVolume", 1f);

        sfxSlider.value = sfxVolume;
        bgmSlider.value = bgmVolume;

        ChangeVolume();
    }
}
