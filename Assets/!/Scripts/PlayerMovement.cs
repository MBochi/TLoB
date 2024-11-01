using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    private Stats playerStats;
    private Vector2 movementInput;
    private bool facingRight = true;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<Stats>();
    }
    //Input
    void Update()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    //Actions
    private void FixedUpdate() 
    {
        Move();
        TurnCheck(movementInput.x);
    }
    public void Move()
    {
        rb.MovePosition(rb.position + playerStats.GetMovementSpeed() * Time.fixedDeltaTime * movementInput);
        animator.SetFloat("moveSpeed", movementInput.sqrMagnitude);
    }

    private void TurnCheck(float move)
    {
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        facingRight = !facingRight;
    }

    public Vector2 GetMovementInput()
    {
        return this.movementInput;
    }
}
