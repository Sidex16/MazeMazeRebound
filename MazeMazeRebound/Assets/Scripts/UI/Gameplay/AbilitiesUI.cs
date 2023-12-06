using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour
{
    public static AbilitiesUI Instance { get; private set; }

    public event EventHandler OnButtonClick;
    public event EventHandler OnTeleportClick;
    public event EventHandler OnHammerClick;
    public event EventHandler OnFingerClick;

    [SerializeField]
    private Button teleport;
    [SerializeField]
    private Button hammer;
    [SerializeField]
    private Button finger;
    [SerializeField]
    private TextMeshProUGUI teleportCount;
    [SerializeField]
    private TextMeshProUGUI hammerCount;
    [SerializeField]
    private TextMeshProUGUI fingerCount;

    SaveManager.PlayerData playerData;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadPlayerData();

        UpdateAbilitiesCount();

        teleport.onClick.AddListener(() =>
        {
            if (playerData.teleportsCount > 0)
            {
                playerData.teleportsCount--;
                GameManager.Instance.SetTeleport(true);
                OnButtonClick?.Invoke(this, EventArgs.Empty);
                OnTeleportClick?.Invoke(this, EventArgs.Empty);
                UpdateAbilitiesCount();
                SaveManager.SavePlayerData(playerData);
            }
        });
        hammer.onClick.AddListener(() =>
        {
            if (playerData.hammersCount > 0)
            {
                playerData.hammersCount--;
                GameManager.Instance.SetHammer(true);
                OnButtonClick?.Invoke(this, EventArgs.Empty);
                OnHammerClick?.Invoke(this, EventArgs.Empty);
                UpdateAbilitiesCount();
                SaveManager.SavePlayerData(playerData);
            }
            
        });
        finger.onClick.AddListener(() =>
        {
            if (playerData.fingersCount > 0)
            {
                playerData.fingersCount--;
                GameManager.Instance.SetFinger(true);
                OnButtonClick?.Invoke(this, EventArgs.Empty);
                OnFingerClick?.Invoke(this, EventArgs.Empty);
                UpdateAbilitiesCount();
                SaveManager.SavePlayerData(playerData);
            }
        });
    }

    private void LoadPlayerData()
    {
        playerData = SaveManager.LoadPlayerData();
    }

    private void UpdateAbilitiesCount()
    {
        teleportCount.text = playerData.teleportsCount.ToString();
        hammerCount.text = playerData.hammersCount.ToString();
        fingerCount.text = playerData.fingersCount.ToString();
    }
}
