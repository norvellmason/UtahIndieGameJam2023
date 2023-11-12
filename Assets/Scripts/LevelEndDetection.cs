using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            GameManager.Instance.GameOver();
        }
        else if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.GameWon();
        }
    }
}
