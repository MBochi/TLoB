using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    private Stats playerStats;
    private float timer = 0f;
    private int damage;
    public int radius;
    public LayerMask damageableLayers;
    private Vector2 stickPos;
    public GameObject attackPoint;
    
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
        CheckCollision();
    }

    public void Setup(int damage)
    {
        this.damage = damage;
        stickPos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float stickAngle = Mathf.Atan2(stickPos.y, stickPos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, stickAngle);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.2f)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void CheckCollision()
    {
        Collider2D[] hitObjecsts = Physics2D.OverlapCircleAll(attackPoint.transform.position, playerStats.GetAttackRadius(), damageableLayers);
        foreach (Collider2D collision_object in hitObjecsts)
        {
            if(collision_object.gameObject.tag == "Enemy")
            {
                collision_object.GetComponent<EnemyCombat>().TakeDamage(this.damage);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.transform.position, playerStats.GetAttackRadius());
    }
}
