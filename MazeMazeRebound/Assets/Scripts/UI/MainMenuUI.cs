using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static event EventHandler OnButtonClick;

    [SerializeField]
    private Button settings;
    [SerializeField]
    private Button shop;
    [SerializeField]
    private Button play;


    private void Awake()
    {
        SaveManager.ClearAllData();

        play.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(SaveManager.LoadPlayerData().lastPlayedLevelIndex);
        });
        settings.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(2);
        });

        shop.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(3);
        });
    }
}
