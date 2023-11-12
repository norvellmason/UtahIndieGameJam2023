using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLogic : MonoBehaviour
{
    [SerializeField] private float Speed = 10;
    private Animator _Animator;

    public void Start()
    {
        GameManager.Instance.AddGameStateSwitchCallback(OnDreamModeChange);
        _Animator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        float speed = Speed;
        if (GameManager.Instance.IsDreamWorld)
            speed *= GameManager.DreamModeSpeedFactor;
        else
            speed = Speed;

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnDreamModeChange()
    {
        if (GameManager.Instance.IsDreamWorld)
        {
            _Animator.SetBool("isDreamWorld", true);
        }
        else
        {
            _Animator.SetBool("isDreamWorld", false);
        }
    }
}
