using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private Sprite _NormalSprite;
    [SerializeField] private Sprite _DreamSprite;
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
            _Renderer.sprite = _DreamSprite;
        }
        else
        {
            _Collider.enabled = false;
            _Renderer.sprite = _NormalSprite;
        }
    }
}
