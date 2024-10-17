using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Stats
{
    private GameObject player;
    private float distance;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField] private bool isChasing;
    [SerializeField] private float aggroRadius;
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isChasing = true;
    }

    void Update()
    {
        Flip();
        Chase();
    }

    public void Chase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < aggroRadius && isChasing) 
        { 
            rb.velocity = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized * GetMovementSpeed();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
          
    }
    private void Flip()
    {
        Vector2 direction = transform.position - player.transform.position;
        if (direction.x > 0) spriteRenderer.flipX = true;
        else if (direction.x < 0) spriteRenderer.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isChasing = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
