using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerGameplay : MonoBehaviour
{
    [SerializeField]
    private AudioClipsSO audioClips;

    private void Start()
    {
        GameplayUI.Instance.OnButtonClick += GameplayUI_OnButtonClick;
        GameplayUI.Instance.OnLevelComplite += GameplayUI_OnLevelComplite;
        PauseUI.Instance.OnButtonClick += PauseUI_OnButtonClick;
        PopUpUI.Instance.OnButtonClick += PopUpUI_OnButtonClick;
        Slingshot.Instance.OnShoot += Slingshot_OnShoot;
        InputHandler.Instance.OnStartShoot += InputHanlder_OnStartShoot;
        Ball.Instance.OnBallHit += Ball_OnBallHit;
    }

    private void Ball_OnBallHit(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.ballHit, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void PopUpUI_OnButtonClick(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.buttonClick, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void PauseUI_OnButtonClick(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.buttonClick, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void InputHanlder_OnStartShoot(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.slingshot, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void Slingshot_OnShoot(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.slingshotShoot, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void GameplayUI_OnLevelComplite(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.gameOver, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }

    private void GameplayUI_OnButtonClick(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(audioClips.buttonClick, Vector3.zero, MusicManager.Instance.GetSoundVolume());
    }


}
