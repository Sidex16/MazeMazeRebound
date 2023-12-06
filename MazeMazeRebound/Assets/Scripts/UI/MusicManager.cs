using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private float musicVolume;
    private float soundVolume;

    private AudioSource audioSource;

    void Awake()
    {
        //SaveManager.ClearAllData();
        musicVolume = 0.15f;
        soundVolume = musicVolume * 8;

        audioSource = GetComponent<AudioSource>();

        audioSource.volume = musicVolume;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        audioSource.volume = volume;
    }
    public float GetMusicVolume() { return musicVolume; }

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
    }

    public float GetSoundVolume() { return soundVolume; }

}

