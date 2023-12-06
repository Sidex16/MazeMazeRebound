using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    public static event EventHandler OnButtonClick;
    public static event EventHandler OnPurchaceFailed;

    [SerializeField]
    private Button buyBall1;
    [SerializeField]
    private Button buyBall2;
    [SerializeField]
    private Button buyBall3;
    [SerializeField]
    private Button buyHammer;
    [SerializeField]
    private Button buyTeleport;
    [SerializeField]
    private Button buyFinger;
    [SerializeField]
    private Button mainMenu;
    [SerializeField]
    TextMeshProUGUI myMoney;

    SaveManager.PlayerData playerData;
    private void Awake()
    {
        LoadData();
        UpdateBalance();

        buyBall1.onClick.AddListener(() =>
        {
            if (playerData.playerMoney > 4000)
            {
                playerData.playerMoney -= 4000;
                playerData.skinIndex = 1;
                SaveManager.SavePlayerData(playerData);
                UpdateBalance();
                OnButtonClick?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPurchaceFailed?.Invoke(this, EventArgs.Empty);
            }

        });

        buyBall2.onClick.AddListener(() =>
        {
            if (playerData.playerMoney > 3000)
            {
                playerData.playerMoney -= 3000;
                playerData.skinIndex = 2;
                SaveManager.SavePlayerData(playerData);
                UpdateBalance();
                OnButtonClick?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPurchaceFailed?.Invoke(this, EventArgs.Empty);
            }

        });

        buyBall3.onClick.AddListener(() =>
        {
            if (playerData.playerMoney > 2000)
            {
                playerData.playerMoney -= 2000;
                playerData.skinIndex = 3;
                SaveManager.SavePlayerData(playerData);
                UpdateBalance();
                OnButtonClick?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPurchaceFailed?.Invoke(this, EventArgs.Empty);
            }

        });

        buyHammer.onClick.AddListener(() =>
        {
            if (playerData.playerMoney > 1000)
            {
                playerData.playerMoney -= 1000;
                playerData.hammersCount++;
                SaveManager.SavePlayerData(playerData);
                UpdateBalance();
                OnButtonClick?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPurchaceFailed?.Invoke(this, EventArgs.Empty);
            }

        });

        buyTeleport.onClick.AddListener(() =>
        {
            if (playerData.playerMoney > 1000)
            {
                playerData.playerMoney -= 1000;
                playerData.teleportsCount++;
                SaveManager.SavePlayerData(playerData);
                UpdateBalance();
                OnButtonClick?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPurchaceFailed?.Invoke(this, EventArgs.Empty);
            }

        });

        buyFinger.onClick.AddListener(() =>
        {
            if (playerData.playerMoney > 1000)
            {
                playerData.playerMoney -= 1000;
                playerData.fingersCount++;
                SaveManager.SavePlayerData(playerData);
                UpdateBalance();
                OnButtonClick?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnPurchaceFailed?.Invoke(this, EventArgs.Empty);
            }

        });

        mainMenu.onClick.AddListener(() =>
        {
            OnButtonClick?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadScene(1);
        });
    }

    private void LoadData()
    {
        playerData = SaveManager.LoadPlayerData();
    }

    private void UpdateBalance()
    {
        myMoney.text = playerData.playerMoney.ToString();
    }

}
