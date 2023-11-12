using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private float time = 0f;


    // Start is called before the first frame update
    void Start()
    {
        time = 0f;        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameOver)
            time += Time.deltaTime;

        textMesh.text = string.Format("{0:0.0}", time);
    }
}
