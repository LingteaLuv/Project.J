using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMoveManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform ground;
    [SerializeField] private LayerMask groundLayer;

    private bool _isGrounded;
    private bool _jumpPressed;
    private Vector2 _normalVec;
    public Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();

        Init();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            _jumpPressed = true;
            animator.SetBool("Jump", true);
        }

        CheckGround();
    }
    
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 moveDir = new Vector2(_normalVec.y, -_normalVec.x);
        moveDir *= h * moveSpeed;
        rigid.velocity = new Vector2(moveDir.x, rigid.velocity.y);
        if (h != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
        }
        animator.SetFloat("Speed", moveDir.magnitude);
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(ground.position, Vector2.down, 0.4f, groundLayer);
        if (hit.collider != null)
        {
            _isGrounded = true;
            _normalVec = hit.normal;
        }
        else
        {
            _isGrounded = false;
            _normalVec = Vector2.up;
        }
    }
    
    private void Jump()
    {
        if (_jumpPressed && _isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            _jumpPressed = false;
            animator.SetBool("Jump", false);
        }
    }

    private void Init()
    {
        rigid.gravityScale = 3f;
       
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        
        if (groundLayer == 0)
        {
            groundLayer = LayerMask.GetMask("Road");
        }
    }
}
