using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0;
    public bool facingRight = true;
    public Transform tf;
    public Camera cam;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;  
    private Vector2 movement;
    private Vector2 mousePos;
    private Vector2 stickPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Input
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //stickPos = new Vector2(Mathf.Round(Input.GetAxisRaw("RightJoyStickHorizontal")), Mathf.Round(Input.GetAxisRaw("RightJoyStickVertical")));
        Debug.Log(stickPos);
    }

    //Actions
    private void FixedUpdate() 
    {
        Move();
        Aim();
        TurnCheck(movement.x);
    }
    public void Move()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
        animator.SetFloat("moveSpeed", movement.sqrMagnitude);
    }

    private void Aim()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //float stickAngle = Mathf.Atan2(stickPos.y, stickPos.x) * Mathf.Rad2Deg;
        tf.rotation = Quaternion.Euler(0, 0, angle);
        //tf.rotation = Quaternion.Euler(0, 0, stickAngle * -1);
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
}
