using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource _musicSource, _effectsSource, _voiceSource, _switchSource;
    private AudioClip _realWorldClip, _dreamWorldClip, _firedClip, _realWalkClip, _dreamWalkClip, _jumpClip, _landingClip, _switchClip, _earlyClip;


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
            _effectsSource.volume = 0.3f;
        }
        if (_voiceSource == null)
            _voiceSource = gameObject.AddComponent<AudioSource>();
        if (_musicSource == null) 
            _musicSource = gameObject.AddComponent<AudioSource>();
        if (_switchSource == null)
            _switchSource = gameObject.AddComponent<AudioSource>();

        LoadClips();
    }

    private void LoadClips()
    {
        _dreamWorldClip = Resources.Load<AudioClip>("Audio/Music/Dark_Theme");
        _realWorldClip = Resources.Load<AudioClip>("Audio/Music/Light_Theme");

        _realWalkClip = Resources.Load<AudioClip>("Audio/SFX/Walking-Light");
        _dreamWalkClip = Resources.Load<AudioClip>("Audio/SFX/Walking-Dark");
        _firedClip = Resources.Load<AudioClip>("Audio/SFX/Fired");
        _jumpClip = Resources.Load<AudioClip>("Audio/SFX/Jump");
        _landingClip = Resources.Load<AudioClip>("Audio/SFX/Landing");
        _switchClip = Resources.Load<AudioClip>("Audio/SFX/Switch");
        _earlyClip = Resources.Load<AudioClip>("Audio/SFX/Early");
    }

    private void PlayVoice(AudioClip clip)
    {
        _voiceSource.PlayOneShot(clip);
    }
    private void PlaySwitch(AudioClip clip)
    {
        if (!_switchSource.isPlaying)
            _switchSource.PlayOneShot(clip);
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

    public void StopMusic()
    {
        _musicSource.Stop();
        _musicSource.clip = null;
    }
    private void StopEffect()
    {
        _effectsSource.Stop();
    }

    public void PlayRealWorldMusic()
    {
        PlayMusic(_realWorldClip);
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

    public void PlayJump()
    {
        StopEffect();
        PlaySound(_jumpClip);
    }
    public void PlayLanding()
    {
        StopEffect();
        PlaySound(_landingClip);
    }
    public void PlaySwitch()
    {
        PlaySwitch(_switchClip);
    }
    public void PlayEarly()
    {
        PlayVoice(_earlyClip);
    }
    public void PlayFired()
    {
        PlayVoice(_firedClip);
    }
}
