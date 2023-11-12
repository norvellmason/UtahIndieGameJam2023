using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private Sprite _NormalSprite;
    [SerializeField] private Sprite _DreamSprite;
    private SpriteRenderer _SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddGameStateSwitchCallback(OnDreamToggle);
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _SpriteRenderer.color = new Color(Random.value * 0.2f + 0.8f, Random.value * 0.2f + 0.8f, Random.value * 0.2f + 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDreamToggle()
    {
        if (GameManager.Instance.IsDreamWorld)
        {
            _SpriteRenderer.sprite = _DreamSprite;
        }
        else
        {
            _SpriteRenderer.sprite = _NormalSprite;
        }
    }
}
