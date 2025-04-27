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
    

    private void Awake()
    {
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
            /*float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); */
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
        }
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
}
