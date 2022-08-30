using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;

    private bool isFacingRight = true;
    private bool isWalking = true;

    private Rigidbody2D rb;
    private Animator anim;


    public float jumpForce = 16.0f;
    public float movementSpeed=10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckmovementDicrection();
        updateAnimations();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
}
