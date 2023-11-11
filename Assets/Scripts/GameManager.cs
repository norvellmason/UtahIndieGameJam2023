using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static float DreamModeSpeedFactor = 0.75f;
    public delegate void GameStateSwitchCallback();
    private List<GameStateSwitchCallback> _GameStateSwitchCallbacks = new List<GameStateSwitchCallback>();

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
        DontDestroyOnLoad(gameObject);
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
