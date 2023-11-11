using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _Rigidbody;
    private Collider2D _Collider;
    private InputController _InputController;
    private SpriteRenderer _SpriteRenderer;
    private Animator _Animator;
    [SerializeField] private float RealMoveSpeed = 100;
    [SerializeField] private float DreamMoveSpeed = 13;
    [SerializeField] private float RealJumpHeight = 1500;
    [SerializeField] private float DreamJumpHeight = 1000;

    private float _JumpCooldown = 0f;
    private float _RecentlyFellTimer = 0.1f;
    private bool _WasGroundedLastFrame = false;

    private float GetMoveSpeed()
    {
        return GameManager.Instance.IsDreamWorld ? DreamMoveSpeed : RealMoveSpeed;
    }

    private float GetJumpHeight()
    {
        return GameManager.Instance.IsDreamWorld ? DreamJumpHeight : RealJumpHeight;
    }

    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _Collider = gameObject.GetComponent<Collider2D>();
        _InputController = gameObject.GetComponent<InputController>();
        _SpriteRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        _Animator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleDreamMode();
        }

        if (_JumpCooldown > 0)
        {
            _JumpCooldown -= Time.deltaTime;
        }

        if (_RecentlyFellTimer > 0)
        {
            _RecentlyFellTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        _Animator.SetBool("isRunning", false);

        if (_InputController.IsPressingRight && !_InputController.IsPressingLeft)
        {
            _Rigidbody.AddForce(new Vector2(GetMoveSpeed(), 0));
            _Animator.SetBool("isRunning", true);
            _SpriteRenderer.flipX = false;
        }

        if(_InputController.IsPressingLeft && !_InputController.IsPressingRight)
        {
            _Rigidbody.AddForce(new Vector2(-GetMoveSpeed(), 0));
            _Animator.SetBool("isRunning", true);
            _SpriteRenderer.flipX = true;
        }

        _Animator.SetBool("isInAir", false);
        if (!IsGrounded())
        {
            _Animator.SetBool("isInAir", true);

            if (_WasGroundedLastFrame)
            {
                _RecentlyFellTimer = 0.04f;
            }
        }

        if (_InputController.IsPressingJump && CanJump())
        {
            _Rigidbody.velocity = new Vector2(_Rigidbody.velocity.x, 0);
            _Rigidbody.AddForce(new Vector2(0, GetJumpHeight()));
            _JumpCooldown = 0.5f;
        }

        _WasGroundedLastFrame = IsGrounded();
    }

    private bool IsGrounded()
    {
        LayerMask mask = LayerMask.GetMask("Obstacles");
        bool hitLeft = Physics2D.Raycast(transform.position + new Vector3(_Collider.bounds.extents.x * 0.9f, 0, 0), Vector2.down, _Collider.bounds.extents.y + 0.1f, mask);
        bool hitMiddle = Physics2D.Raycast(transform.position, Vector2.down, _Collider.bounds.extents.y + 0.1f, mask);
        bool hitRight = Physics2D.Raycast(transform.position - new Vector3(_Collider.bounds.extents.x * 0.9f, 0, 0), Vector2.down, _Collider.bounds.extents.y + 0.1f, mask);
        return hitLeft || hitMiddle || hitRight;
    }

    private bool CanJump()
    {
        if (_JumpCooldown > 0)
        {
            return false;
        }

        if (!IsGrounded() && _RecentlyFellTimer <= 0)
        {
            return false;
        }

        return true;
    }

    private void ToggleDreamMode()
    {
        GameManager.Instance.ToggleDreamMode(); 
        
        if (GameManager.Instance.IsDreamWorld)
        {
            _Rigidbody.gravityScale = 0.75f;
            _Rigidbody.drag = 3;
            _Animator.SetBool("isDreamMode", true);
        }
        else
        {
            _Rigidbody.gravityScale = 8;
            _Rigidbody.drag = 10;
            _Animator.SetBool("isDreamMode", false);
        }
    }
}
