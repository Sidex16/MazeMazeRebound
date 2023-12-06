using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance { get; private set; }

    public event EventHandler OnButtonClick;

    [SerializeField]
    private Button restart;
    [SerializeField]
    private Button resume;
    [SerializeField]
    private Button exit;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        restart.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        resume.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        });
        exit.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(1);
        });
    }
}
