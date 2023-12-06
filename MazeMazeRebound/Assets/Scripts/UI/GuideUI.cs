using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SaveManager;

public class GuideUI : MonoBehaviour
{
    [SerializeField]
    private GameObject image1;
    [SerializeField]
    private GameObject image2;
    [SerializeField] 
    private Button start1;
    [SerializeField]
    private Button start2;

    private SaveManager.PlayerData playerData;

    private void Awake()
    {
        playerData = SaveManager.LoadPlayerData();
        start1.onClick.AddListener(() =>
        {
            image1.SetActive(false);
        });
        start2.onClick.AddListener(() =>
        {
            image2.SetActive(false);
        });
        if (playerData.isFitrsPlay)
        {
            gameObject.SetActive(true);
            playerData.isFitrsPlay = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
        SaveManager.SavePlayerData(playerData);
    }
}
