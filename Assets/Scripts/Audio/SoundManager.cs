using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource _musicSource, _effectsSource;


    public static void Create()
    {
        GameObject soundManager = new GameObject("SoundManager");
        soundManager.AddComponent<SoundManager>();
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (_effectsSource == null)
            _effectsSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void PlayFired()
    {
        AudioClip firedClip = Resources.Load<AudioClip>("Audio/SFX/fired");
        PlaySound(firedClip);
    }
}
