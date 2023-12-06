using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private Slider loadingBar;
    [SerializeField]
    private TextMeshProUGUI loadingText;

    private void Awake()
    {
        loadingBar.value = 0;
        loadingText.text = "Loading..." + loadingBar.value + "%";
    }

    private void Update()
    {
        loadingBar.value += 1f;
        loadingText.text = "Loading..." + (loadingBar.value) + "%";
        if (loadingBar.value >= 100)
        {
            SceneManager.LoadScene(1);
        }
    }
}
