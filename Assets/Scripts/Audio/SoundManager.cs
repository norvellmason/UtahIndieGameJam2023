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
            _dreamWorldClip = Resources.Load<AudioClip>("Audio/Music/Dark_Theme");
        if (_realWorldClip == null)
            _realWorldClip = Resources.Load<AudioClip>("Audio/Music/Dark_Theme");

        if (_firedClip == null)
            _firedClip = Resources.Load<AudioClip>("Audio/SFX/fired");
    }

    private void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (_musicSource.clip != clip)
        {
            float currentMusicPosition = _musicSource.time;
            _musicSource.Stop();

            _musicSource.clip = clip;
            _musicSource.time = currentMusicPosition;
            _musicSource.Play();
        }
    }

    public void PlayRealWorldMusic()
    {
        PlayMusic(_realWorldClip);
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
