using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float jumpForce= 10.0f;
    [SerializeField]
    private Transform feetPos;
    [SerializeField]
    private float checkRadius;
    private Rigidbody2D rb;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float jumpTime = .25f;
    private float jumpTimeCount;
    private bool isJumping;
    [SerializeField]
    private float dashSpeed;
    private float dashTime;
    [SerializeField]
    private float startDashTime;
    [SerializeField]
    private int direction;
    private float dashCooldown = 2;

    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);                              // Posição Inicial

        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;

    }

    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");                    // Movimento básico                                    
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        PlayerJump();
        PlayerDash();
        
    }

    void PlayerJump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true;
            jumpTimeCount = jumpTime;

        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCount > 0)
            {
                rb.velocity = Vector3.up * jumpForce;
                jumpTimeCount -= Time.deltaTime;

            }
            else
            {
                isJumping = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
    void PlayerDash()
    {
        if (direction == 0)
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 1;

            }
            if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 2;
            }

        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector3.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
            }
            if(direction == 1)
            {
                rb.velocity = Vector3.left * dashSpeed;
            }
            if (direction == 2)
            {
                rb.velocity = Vector3.right * dashSpeed;
            }
        }
    }

}


