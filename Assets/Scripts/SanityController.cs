using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    [SerializeField]
    private int MaxSanity = 100;
    [SerializeField]
    private int SanityRegen = 1;
    private float _currentSanity = 0;

    private float CurrentSanity {
        get { return _currentSanity;  }
        set
        {
            _currentSanity = value;
            GameManager.Instance.SetSanity(value);
        }
    }


    private void Start()
    {
        CurrentSanity = MaxSanity;
    }

    private void Update()
    {
        if (GameManager.Instance.IsRealWorld)
            IncreaseSanity();
        else if (GameManager.Instance.IsDreamMode)
            DecreaseSanity();
    }

    private void IncreaseSanity()
    {
        if (CurrentSanity < MaxSanity)
            CurrentSanity += SanityRegen * Time.deltaTime;
        else if (CurrentSanity > MaxSanity)
            CurrentSanity = MaxSanity;
    }

    private void DecreaseSanity()
    {
        if (CurrentSanity > 0)
            CurrentSanity -= SanityRegen * Time.deltaTime;
        else
            GameManager.Instance.SetPlayerIsInsane();
    }
}
