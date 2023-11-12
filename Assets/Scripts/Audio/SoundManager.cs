using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource _musicSource, _effectsSource, _voiceSource, _switchSource;
    private AudioClip _realWorldClip, _dreamWorldClip, _firedClip, _realWalkClip, _dreamWalkClip, _jumpClip, _landingClip, _switchClip, _earlyClip;
    private IEnumerator _musicCoroutine;
    private bool _playSound = true;


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
        {
            _switchSource = gameObject.AddComponent<AudioSource>();
            _switchSource.volume = 0.3f;
        }

        LoadClips();
    }

    private void LoadClips()
    {
        _dreamWorldClip = Resources.Load<AudioClip>("Audio/Music/Light_Theme_Darkened");
        _realWorldClip = Resources.Load<AudioClip>("Audio/Music/Light_Theme");

        _realWalkClip = Resources.Load<AudioClip>("Audio/SFX/Walking-Light");
        _dreamWalkClip = Resources.Load<AudioClip>("Audio/SFX/Walking-Dark");
        _firedClip = Resources.Load<AudioClip>("Audio/SFX/Fired");
        _jumpClip = Resources.Load<AudioClip>("Audio/SFX/Jump");
        _landingClip = Resources.Load<AudioClip>("Audio/SFX/Landing");
        _switchClip = Resources.Load<AudioClip>("Audio/SFX/Switch");
        _earlyClip = Resources.Load<AudioClip>("Audio/SFX/Early");
    }

    public void ToggleSound()
    {
        _playSound = !_playSound;
        _musicSource.mute = !_playSound;
        if (!_playSound) {
            StopEffect();
        }
    }

    private void PlayVoice(AudioClip clip)
    {
        if (_playSound)
            _voiceSource.PlayOneShot(clip);
    }
    private void PlaySwitch(AudioClip clip)
    {
        if (_playSound && !_switchSource.isPlaying)
            _switchSource.PlayOneShot(clip);
    }
    private void PlaySound(AudioClip clip)
    {
        if (_playSound && !_effectsSource.isPlaying)
            _effectsSource.PlayOneShot(clip);
    }

    private void PlayMusicInternal(AudioClip clip)
    {
        if (_playSound)
        {
            if (_musicSource.clip == null)
            {
                _musicSource.clip = clip;
                _musicSource.Play();
            }
            else if (_musicSource.clip != clip)
            {
                if (_musicCoroutine != null)
                    StopCoroutine(_musicCoroutine);

                _musicCoroutine = AudioFade.FadeOutAndIn(_musicSource, clip, 0.01f, 1f);
                StartCoroutine(_musicCoroutine);
            }
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

    public void PlayMusic()
    {
        if (GameManager.Instance.IsRealWorld)
            PlayMusicInternal(_realWorldClip);
        else if (GameManager.Instance.IsDreamWorld)
            PlayMusicInternal(_dreamWorldClip);
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
