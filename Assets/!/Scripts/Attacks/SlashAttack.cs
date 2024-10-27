using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    private Stats playerStats;
    private float timer = 0f;
    private int damage;
    public LayerMask damageableLayers;
    private Vector2 stickPos;
    private GameObject attackPoint;
    
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
        CheckCollision();
    }

    public void Setup(int damage, GameObject attackPoint)
    {
        this.damage = damage;
        this.attackPoint = attackPoint;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= .2f)
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
                collision_object.GetComponent<EnemyCombat>().TakeDamage(this.damage, true);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.transform.position, playerStats.GetAttackRadius());
    }
}
