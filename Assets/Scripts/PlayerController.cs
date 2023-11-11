using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _Rigidbody;
    private Collider2D _Collider;
    private InputController _InputController;
    private SpriteRenderer _SpriteRenderer;
    private float RealMoveSpeed = 100;
    private float DreamMoveSpeed = 13;
    private float RealJumpHeight = 1500;
    private float DreamJumpHeight = 600;

    private float _JumpCooldown = 0f;

    private float GetMoveSpeed()
    {
        return GameManager.Instance.IsDreamMode ? DreamMoveSpeed : RealMoveSpeed;
    }

    private float GetJumpHeight()
    {
        return GameManager.Instance.IsDreamMode ? DreamJumpHeight : RealJumpHeight;
    }

    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _Collider = gameObject.GetComponent<Collider2D>();
        _InputController = gameObject.GetComponent<InputController>();
        _SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleDreamMode();
        }

        if (_JumpCooldown > 0)
        {
            _JumpCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_InputController.IsPressingRight && !_InputController.IsPressingLeft)
        {
            _Rigidbody.AddForce(new Vector2(GetMoveSpeed(), 0));
        }

        if(_InputController.IsPressingLeft && !_InputController.IsPressingRight)
        {
            _Rigidbody.AddForce(new Vector2(-GetMoveSpeed(), 0));
        }

        if (_InputController.IsPressingJump && IsGrounded() && _JumpCooldown <= 0)
        {
            _Rigidbody.velocity = new Vector2(_Rigidbody.velocity.x, 0);
            _Rigidbody.AddForce(new Vector2(0, GetJumpHeight()));
            _JumpCooldown = 0.5f;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, _Collider.bounds.extents.y + 0.1f);
    }

    private void ToggleDreamMode()
    {
        GameManager.Instance.ToggleDreamMode(); 
        
        if (GameManager.Instance.IsDreamMode)
        {
            _Rigidbody.gravityScale = 0.75f;
            _Rigidbody.drag = 3;
            _SpriteRenderer.color = Color.black;
        }
        else
        {
            _Rigidbody.gravityScale = 8;
            _Rigidbody.drag = 10;
            _SpriteRenderer.color = Color.white;
        }
    }
}
