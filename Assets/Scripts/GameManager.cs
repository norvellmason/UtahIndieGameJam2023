using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
        UpsideDownWorld
    }

    private State state;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else 
            Instance = this;
    }

    private void Start()
    {
        state = State.RealWorld;
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


    public void SetPlayerIsInsane()
    {
        // Show game over 
    }
}
