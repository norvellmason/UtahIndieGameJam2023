using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _normalSprite;
    [SerializeField] private SpriteRenderer _dreamSprite;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddGameStateSwitchCallback(OnDreamToggle);
        OnDreamToggle();
    }

    private void OnDreamToggle()
    {
        if (GameManager.Instance.IsDreamWorld)
        {
            _normalSprite.gameObject.SetActive(false);
            _dreamSprite.gameObject.SetActive(true);
        }
        else
        {
            _normalSprite.gameObject.SetActive(true);
            _dreamSprite.gameObject.SetActive(false);
        }
    }
}
