using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;

    private int amountOfJumpsLeft;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canJump;

    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJump = 1;

    public float jumpForce = 16.0f;
    public float movementSpeed=10.0f;
    public float groundCheckRadius;

    public Transform groundCheck;

    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJump;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckmovementDicrection();
        updateAnimations();
        CheckIfCanJump();
    }


    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJump;
        }

        canJump = amountOfJumpsLeft > 0;
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckmovementDicrection()
    {
        if (isFacingRight && movementInputDirection<0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection>0)
        {
            Flip();
        }

        isWalking = rb.velocity.x != 0;
    }

    private void updateAnimations(){
        anim.SetBool("isWalking",isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity",rb.velocity.y);

    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementSpeed*movementInputDirection, rb.velocity.y);
    }
    
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
