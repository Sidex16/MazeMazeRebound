using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SaveManager;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance { get; private set; }

    public event EventHandler OnButtonClick;
    public event EventHandler OnLevelComplite;

    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private GameObject pause;
    [SerializeField]
    private GameObject popUp;

    private SaveManager.PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        playerData = SaveManager.LoadPlayerData();

        pauseButton.onClick.AddListener(() =>
        {
            pause.SetActive(true);
            OnButtonClick?.Invoke(this, EventArgs.Empty);
        });
    }

    private void Start()
    {
        HideInterface();
        Finish.OnFinishTrigger += Finish_OnFinishTrigger;
    }

    private void Finish_OnFinishTrigger(object sender, System.EventArgs e)
    {
        popUp.SetActive(true);
        OnLevelComplite?.Invoke(this, EventArgs.Empty);
        playerData.playerMoney += popUp.GetComponent<PopUpUI>().GetCoinReward();
        playerData.lastPlayedLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SaveManager.SavePlayerData(playerData);
    }

    private void HideInterface()
    {
        pause.SetActive(false);
        popUp.SetActive(false);
    }

    private void OnDisable()
    {
        Finish.OnFinishTrigger -= Finish_OnFinishTrigger;
    }
}
