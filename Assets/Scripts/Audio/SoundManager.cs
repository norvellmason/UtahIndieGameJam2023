using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource _musicSource, _effectsSource;
    private AudioClip _realWorldClip, _dreamWorldClip, _firedClip, _realWalkClip, _dreamWalkClip;


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
        {
            _effectsSource = gameObject.AddComponent<AudioSource>();
            _effectsSource.volume = 0.5f;
        }
        if (_musicSource == null) 
            _musicSource = gameObject.AddComponent<AudioSource>();

        LoadClips();
    }

    private void LoadClips()
    {
        _dreamWorldClip = Resources.Load<AudioClip>("Audio/Music/Dark_Theme");
        _realWorldClip = Resources.Load<AudioClip>("Audio/Music/Dark_Theme");

        _realWalkClip = Resources.Load<AudioClip>("Audio/SFX/Walking-Light");
        _dreamWalkClip = Resources.Load<AudioClip>("Audio/SFX/Walking-Dark");
        _firedClip = Resources.Load<AudioClip>("Audio/SFX/fired");
    }

    private void PlaySound(AudioClip clip)
    {
        if (!_effectsSource.isPlaying)
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

    public void PlayFired()
    {
        PlaySound(_firedClip);
    }

    public void PlayDreamWorldMusic()
    {
        PlayMusic(_dreamWorldClip);
    }

    public void PlayWalk()
    {
        if (GameManager.Instance.IsRealWorld)
            PlayRealWalk();
        else
            PlayDreamWalk();
    }

    private void PlayRealWalk()
    {
        PlaySound(_realWalkClip);
    }
    private void PlayDreamWalk()
    {
        PlaySound(_dreamWalkClip);
    }
}
