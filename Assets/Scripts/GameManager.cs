using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    private enum State
    {
        RealWorld,
        UpsideDownWorld
    }

    private State state;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
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

    public bool IsUpsideDownWorld
    {
        get { return state == State.UpsideDownWorld; }
    }


    public void SetPlayerIsInsane()
    {
        // Show game over 
    }
}
