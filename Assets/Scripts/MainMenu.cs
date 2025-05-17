using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;

    [SerializeField] GameObject quitButton;

    private static bool isInitialized = false;

    private void Start()
    {
        if (!isInitialized)
        {
            SetMasterVolume(masterSlider.value);
            SetMusicVolume(musicSlider.value);
            isInitialized = true;
        }
        else
        {
            masterSlider.value = Mathf.Pow(10, (AudioManager.Instance.GetMasterVolume() / 20));
            musicSlider.value = Mathf.Pow(10, (AudioManager.Instance.GetMusicVolume() / 20));
        }

#if UNITY_WEBGL
        quitButton.SetActive(false);
#endif
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }
}
