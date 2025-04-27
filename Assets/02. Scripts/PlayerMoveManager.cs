using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform ground;
    [SerializeField] private LayerMask _groundLayer;

    private bool _isGrounded;
    private bool _jumpPressed;
    private Vector2 _normalVec;
    public Animator animator;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        if (_groundLayer == null)
        {
            _groundLayer = LayerMask.GetMask("Road");
        }
        
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
        jumpCheck();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 moveDir = new Vector2(_normalVec.y, -_normalVec.x);
        moveDir *= h * moveSpeed;
        rigid.velocity = new Vector2(moveDir.x, rigid.velocity.y);
        if (h != 0)
        {
            /*float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); */
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
        }
        animator.SetFloat("Speed", moveDir.magnitude);
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(ground.position, Vector2.down, 0.8f, _groundLayer);
        if (hit.collider != null)
        {
            _isGrounded = true;
            _normalVec = hit.normal;
        }
        else
        {
            _isGrounded = false;
            _normalVec = Vector2.up;
            rigid.gravityScale = 3f;
        }
    }
    
    private void Jump()
    {
        if (_jumpPressed && _isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            _jumpPressed = false;
        }
    }
    
    private void jumpCheck()
    {
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(ground.position, Vector2.down, Color.green);
            RaycastHit2D rayHit = Physics2D.Raycast(ground.position, Vector2.down, 0.5f);
            if (rayHit.collider != null)
            {
                if(rayHit.distance < 0.01f)
                    animator.SetBool("Jump", false);
            }
        }
    }
}
