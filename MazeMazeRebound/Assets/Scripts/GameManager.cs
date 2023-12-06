using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isGameOver;
    private bool isTeleportActive;
    private bool isHammerActive;
    private bool isFingerActive;

    private SaveManager.PlayerData playerData;

    private void Awake()
    {
        

        if (Instance == null)
        {
            Instance = this;
        }
        
        SaveCurrentLevelIndex();
    }

    private void SaveCurrentLevelIndex()
    {
        playerData = SaveManager.LoadPlayerData();
        playerData.lastPlayedLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SaveManager.SavePlayerData(playerData);
    }

    public void SetGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
    }

    public void SetTeleport(bool isTeleportActive)
    {
        this.isTeleportActive = isTeleportActive;
    }

    public void SetHammer(bool isHammerActive)
    {
        this.isHammerActive = isHammerActive;
    }

    public void SetFinger(bool isFingerActive)
    {
        this.isFingerActive = isFingerActive;
    }

    public bool GetIsGameOver() {  return isGameOver; }

    public bool GetIsTeleportActive() { return isTeleportActive; }

    public bool GetIsHammerActive() {  return isHammerActive; }

    public bool GetIsFingerActive() {  return isFingerActive; }
}
