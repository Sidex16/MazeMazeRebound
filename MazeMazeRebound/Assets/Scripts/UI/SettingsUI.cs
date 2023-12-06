using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsUI : MonoBehaviour
{
    public static event EventHandler OnButtonClick;

    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider soundSlider;
    [SerializeField]
    private Button mainMenu;

    private void Awake()
    {
        mainMenu.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(1);
        });
    }

    private void Start()
    {
        musicSlider.value = MusicManager.Instance.GetMusicVolume();
        soundSlider.value = MusicManager.Instance.GetSoundVolume();
    }

    private void Update()
    {
        MusicManager.Instance.SetMusicVolume(musicSlider.value);
        MusicManager.Instance.SetSoundVolume(soundSlider.value * 2);
    }
}
