using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCount : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI levelCount;

    private SaveManager.PlayerData playerData;

    private void Start()
    {
        playerData = SaveManager.LoadPlayerData();

        levelCount.text = "Level " + playerData.levelCount;
    }
}
