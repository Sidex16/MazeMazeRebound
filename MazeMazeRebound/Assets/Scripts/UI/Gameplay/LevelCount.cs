using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCount : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] levelCountList;

    private SaveManager.PlayerData playerData;

    private void Start()
    {
        playerData = SaveManager.LoadPlayerData();

        foreach (var levelCount in levelCountList)
        {
            levelCount.text = playerData.levelCount.ToString();

        }
    }
}
