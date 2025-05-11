using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
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
}
