using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLogic : MonoBehaviour
{
    [SerializeField] private float Speed = 10;

    private void Update()
    {
        float speed = Speed;
        if (GameManager.Instance.IsDreamWorld)
            speed *= GameManager.DreamModeSpeedFactor;
        else
            speed = Speed;

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
