using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;        // Untuk suara efek (bell, telepon, dll.)
    public AudioSource typingSource;     // Sumber audio terpisah untuk efek typing yang berulang
    public AudioSource musicSource;      // Untuk musik latar belakang

    [Header("Sound Effects")]
    public AudioClip phoneSFX;
    public AudioClip bellSFX;
    public AudioClip typingSFX;          // Efek suara typing

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fungsi untuk memainkan efek suara tertentu satu kali
    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "Phone":
                if (phoneSFX != null) sfxSource.PlayOneShot(phoneSFX);
                break;
            case "Bell":
                if (bellSFX != null) sfxSource.PlayOneShot(bellSFX);
                break;
                // Tambahkan efek suara lainnya jika diperlukan
        }
    }

    // Fungsi untuk memulai atau menghentikan efek suara typing yang berulang
    public void PlayTypingSFX()
    {
        if (typingSFX != null && !typingSource.isPlaying)
        {
            typingSource.clip = typingSFX;
            typingSource.loop = true; // Looping untuk typing
            typingSource.Play();
        }
    }

    public void StopTypingSFX()
    {
        if (typingSource.isPlaying)
        {
            typingSource.Stop();
        }
    }

    // Fungsi untuk memainkan musik latar
    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        musicSource.clip = musicClip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
