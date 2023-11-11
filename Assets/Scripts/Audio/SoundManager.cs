using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource _musicSource, _effectsSource;
    private AudioClip _firedClip, _realWorldClip, _dreamWorldClip;


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
        if (_musicSource == null) 
            _musicSource = gameObject.AddComponent<AudioSource>();

        LoadClips();
    }

    private void LoadClips()
    {
        if (_dreamWorldClip == null)
        {
            _dreamWorldClip = Resources.Load<AudioClip>("Audio/Music/Dark_Theme");
            PreloadMusicClip(_dreamWorldClip);
        }

        if (_firedClip == null)
            _firedClip = Resources.Load<AudioClip>("Audio/SFX/fired");
    }

    /// <summary>
    /// Preload playing music so that the first time the user plays it, it doesn't cause any lag
    /// </summary>
    /// <param name="clip"></param>
    private void PreloadMusicClip(AudioClip clip)
    {
        float prevVolume = _musicSource.volume;
        _musicSource.volume = 0;
        PlayMusic(clip);
        _musicSource.Stop();
        _musicSource.volume = prevVolume;
    }

    private void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    private void PlayMusic(AudioClip clip)
    {
        _musicSource.PlayOneShot(clip);
    }

    public void PlayDreamWorldMusic()
    {
        PlayMusic(_dreamWorldClip);
    }

    public void PlayFired()
    {
        PlaySound(_firedClip);
    }
}
