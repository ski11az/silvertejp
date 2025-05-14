using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// Plays a clip once. Volume can be optionally set to [0-1].
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    public void PlayClip(AudioClip clip, float volume = 1)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(value*2) * 20);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value*2) * 20);
    }

    public float GetMasterVolume()
    {
        mixer.GetFloat("MaseterVolume", out float val);
        return val;
    }

    public float GetMusicVolume()
    {
        mixer.GetFloat("MusicVolume", out float val);
        return val;
    }
}
