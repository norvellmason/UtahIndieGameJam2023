using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool IsPressingJump { get; private set; }
    public bool IsPressingRight { get; private set; }
    public bool IsPressingLeft { get; private set; }
    public bool IsPressingDown { get; private set; }

    // Update is called once per frame
    void Update()
    {
        IsPressingJump = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        IsPressingRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        IsPressingLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        IsPressingDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        if (Input.GetKeyDown(KeyCode.C))
            GameManager.DebugInvincible = true;
        else if (Input.GetKeyDown(KeyCode.V))
            GameManager.DebugInvincible = false;
    }
}
