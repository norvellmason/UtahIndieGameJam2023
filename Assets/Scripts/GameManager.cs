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
    public static bool ALLOW_CHEATS = true;
    public static bool _debugInvincible = false;

    public delegate void GameStateSwitchCallback();
    private List<GameStateSwitchCallback> _GameStateSwitchCallbacks = new List<GameStateSwitchCallback>();
    public event EventHandler<SanityEventArgs> OnSanityChanged;

    private enum GameManagerState
    {
        RealWorld,
        DreamWorld,
        GameOver
    }

    private GameManagerState _state;


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
        State = GameManagerState.RealWorld;
    }

    public static bool DebugInvincible
    {
        get { return _debugInvincible; }
        set 
        { 
            if (ALLOW_CHEATS)
                _debugInvincible = value;
        }
    }

    private GameManagerState State
    {
        get { return _state; }
        set 
        { 
            _state = value;
            switch (value)
            {
                case GameManagerState.GameOver:
                    Time.timeScale = 0f;
                    SoundManager.Instance.StopMusic();
                    break;
                case GameManagerState.RealWorld:
                    SoundManager.Instance.PlayRealWorldMusic();
                    break;
                case GameManagerState.DreamWorld:
                    SoundManager.Instance.PlayDreamWorldMusic();
                    break;
            }
            if (value != GameManagerState.GameOver)
                Time.timeScale = 1f;
        }
    }

    public bool IsGameOver
    {
        get { return State == GameManagerState.GameOver; }
    }

    public bool IsRealWorld
    {
        get { return State == GameManagerState.RealWorld; }
    }

    public bool IsDreamWorld
    {
        get { return State == GameManagerState.DreamWorld; }
    }

    public void SetSanity(float currentSanity)
    {
        OnSanityChanged?.Invoke(this, new SanityEventArgs(currentSanity));
    }


    public void ToggleDreamMode()
    {
        if (State == GameManagerState.RealWorld)
            State = GameManagerState.DreamWorld;
        else
            State = GameManagerState.RealWorld;

        foreach (GameStateSwitchCallback callback in _GameStateSwitchCallbacks)
        {
            callback();
        }
    }

    public void AddGameStateSwitchCallback(GameStateSwitchCallback callback)
    {
        _GameStateSwitchCallbacks.Add(callback);
    }

    public void GameOver()
    {
        if (!GameManager.Instance.IsGameOver && !GameManager.DebugInvincible)
        {
            State = GameManagerState.GameOver;
            Time.timeScale = 0f;
            SoundManager.Instance.PlayFired();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GameWon()
    {
        State = GameManagerState.GameOver;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
