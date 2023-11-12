using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SanityUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Start()
    {
        GameManager.Instance.OnSanityChanged += GameManager_OnSanityChanged; 
    }

    private void GameManager_OnSanityChanged(object sender, SanityEventArgs e)
    {
        textMesh.text = string.Format("{0:0}", e.CurrentSanity);
    }
}
