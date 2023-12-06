using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerShop : MonoBehaviour
{
    [SerializeField]
    private AudioClipsSO audioClips;


    private void OnEnable()
    {
        ShopUI.OnButtonClick += MainMenuUI_OnButtonClick;
        ShopUI.OnPurchaceFailed += ShopUI_OnPurchaceFailed;
    }

    private void ShopUI_OnPurchaceFailed(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.purchaseFail, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void MainMenuUI_OnButtonClick(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.buttonClick, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void OnDisable()
    {
        ShopUI.OnButtonClick -= MainMenuUI_OnButtonClick;
    }
}
