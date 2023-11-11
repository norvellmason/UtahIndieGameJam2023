using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    [SerializeField]
    private int MaxSanity = 100;
    [SerializeField]
    private int SanityRegen = 1;
    private float CurrentSanity = 0;


    private void Start()
    {
        CurrentSanity = MaxSanity;
    }

    private void Update()
    {
        if (GameManager.Instance.IsRealWorld)
            IncreaseSanity();
        else if (GameManager.Instance.IsUpsideDownWorld)
            DecreaseSanity();
    }

    private void IncreaseSanity()
    {
        if (CurrentSanity < MaxSanity) 
            CurrentSanity += SanityRegen * Time.deltaTime;
    }

    private void DecreaseSanity()
    {
        if (CurrentSanity > 0)
            CurrentSanity -= SanityRegen * Time.deltaTime;
        else
            GameManager.Instance.SetPlayerIsInsane();
    }
}
