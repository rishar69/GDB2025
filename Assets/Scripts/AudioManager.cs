using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] soundEffects;
    [SerializeField] private Sound[] backgroundMusic;

    private AudioSource sfxSource;
    private AudioSource bgmSource;

    private string bgmActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeAudioSources();
    }

    private void OnEnable()
    {
        AudioSetting.OnSettingChanged += UpdateVolume;
    }

    private void OnDisable()
    {
        AudioSetting.OnSettingChanged -= UpdateVolume;
    }

    private void InitializeAudioSources()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>();

        sfxSource.playOnAwake = false;
        sfxSource.loop = false;

        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
    }

    private void UpdateVolume(float sfxVolume, float bgmVolume)
    {
        sfxSource.volume = sfxVolume;
        bgmSource.volume = bgmVolume;
    }

    public void PlaySfx(string name)
    {
        Sound sound = Array.Find(soundEffects, s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning($"SFX '{name}' not found!");
            return;
        }
        sfxSource.PlayOneShot(sound.clip);
    }

    public void PlayBgm(string name)
    {
        if (bgmActive == name)
            return;

        Sound sound = Array.Find(backgroundMusic, s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning($"BGM '{name}' not found!");
            return;
        }
        bgmSource.clip = sound.clip;
        bgmSource.Play();
        bgmActive = name;
    }

    public void StopBgm()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

