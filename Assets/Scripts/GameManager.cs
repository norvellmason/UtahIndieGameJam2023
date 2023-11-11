using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SanityEventArgs : EventArgs
{
    public float CurrentSanity { get; set; }
    
    public SanityEventArgs(float currentSanity)
    {
        CurrentSanity = currentSanity;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static float DreamModeSpeedFactor = 0.75f;
    public delegate void GameStateSwitchCallback();
    private List<GameStateSwitchCallback> _GameStateSwitchCallbacks = new List<GameStateSwitchCallback>();
    public event EventHandler<SanityEventArgs> OnSanityChanged;

    private enum State
    {
        RealWorld,
        UpsideDownWorld,
        GameOver
    }

    private State _state;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;

        if (SoundManager.Instance == null)
        {
            SoundManager.Create();
        }
    }

    private void Start()
    {
        state = State.RealWorld;
    }

    private State state
    {
        get { return _state; }
        set 
        { 
            _state = value;
            if (value != State.GameOver)
                Time.timeScale = 1f;
        }
    }

    public bool IsGameOver
    {
        get { return state == State.GameOver; }
    }

    public bool IsRealWorld
    {
        get { return state == State.RealWorld; }
    }

    public bool IsDreamMode
    {
        get { return state == State.UpsideDownWorld; }
    }

    public void SetSanity(float currentSanity)
    {
        OnSanityChanged?.Invoke(this, new SanityEventArgs(currentSanity));
    }


    public void ToggleDreamMode()
    {
        if (state == State.RealWorld)
            state = State.UpsideDownWorld;
        else
            state = State.RealWorld;

        foreach (GameStateSwitchCallback callback in _GameStateSwitchCallbacks)
        {
            callback();
        }
    }

    public void AddGameStateSwitchCallback(GameStateSwitchCallback callback)
    {
        _GameStateSwitchCallbacks.Add(callback);
    }

    private void KillPlayer()
    {
        SoundManager.Instance.PlayFired();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            state = State.GameOver;
            Time.timeScale = 0f;
            KillPlayer();
        }
    }
}
