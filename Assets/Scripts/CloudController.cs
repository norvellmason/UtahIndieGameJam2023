using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    BoxCollider2D _Collider;
    SpriteRenderer _Renderer;

    // Start is called before the first frame update
    void Start()
    {
        _Collider = GetComponent<BoxCollider2D>();
        _Renderer = GetComponent<SpriteRenderer>();
        GameManager.Instance.AddGameStateSwitchCallback(GameStateSwitchCallback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameStateSwitchCallback()
    {
        if (GameManager.Instance.IsDreamWorld)
        {
            _Collider.enabled = true;
            _Renderer.color = Color.green;
        }
        else
        {
            _Collider.enabled = false;
            _Renderer.color = Color.white * 0.5f;
        }
    }
}
