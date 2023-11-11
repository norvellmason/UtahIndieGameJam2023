using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10;

   // Update is called once per frame
    void Update()
    {
        float speed = Speed;
        if (GameManager.Instance.IsDreamMode)
            speed *= GameManager.DreamModeSpeedFactor;
        else
            speed = Speed;

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
