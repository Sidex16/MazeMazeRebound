using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour
{
    public static PopUpUI Instance { get; private set; }

    public event EventHandler OnButtonClick;

    [SerializeField]
    private Button home;
    [SerializeField]
    private Button restart;
    [SerializeField]
    private Button next;
    [SerializeField]
    private TextMeshProUGUI coinAmount;

    private SaveManager.PlayerData playerData;

    int coinReward;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        GetData();
        GetReward();

        home.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(1);
        });
        restart.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        next.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            if (SceneManager.GetActiveScene().buildIndex == 11)
            {
                SceneManager.LoadScene(4);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        });
    }
    private void GetReward()
    {
        coinReward = UnityEngine.Random.Range(1000, 2001);
        coinAmount.text = coinReward.ToString();    
    }

    private void GetData()
    {
        playerData = SaveManager.LoadPlayerData();
    }

    public int GetCoinReward()
    {
        return coinReward;
    }
}
