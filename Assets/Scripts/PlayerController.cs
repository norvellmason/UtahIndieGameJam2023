using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _Rigidbody;
    private Collider2D _Collider;
    private InputController _InputController;
    public float MoveSpeed = 100;
    public float JumpHeight = 1000;

    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _Collider = gameObject.GetComponent<Collider2D>();
        _InputController = gameObject.GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_InputController.IsPressingRight && !_InputController.IsPressingLeft)
        {
            _Rigidbody.AddForce(new Vector2(MoveSpeed, 0));
        }

        if(_InputController.IsPressingLeft && !_InputController.IsPressingRight)
        {
            _Rigidbody.AddForce(new Vector2(-MoveSpeed, 0));
        }

        if (_InputController.IsPressingJump && IsGrounded())
        {
            _Rigidbody.AddForce(new Vector2(0, JumpHeight));
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, _Collider.bounds.extents.y + 0.2f);
    }
}
