using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AIChase : MonoBehaviour
{
    private GameObject player;
    private float distance;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float aggroRadius;


    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Chase();
        rb.velocity = Vector3.zero;
    }

    public void Chase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Flip();

        if (distance < aggroRadius)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void Flip()
    {
        Vector2 direction = transform.position - player.transform.position;
        if (direction.x > 0) spriteRenderer.flipX = true;
        else if (direction.x < 0) spriteRenderer.flipX = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
