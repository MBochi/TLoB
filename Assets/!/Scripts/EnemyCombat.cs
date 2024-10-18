using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Stats enemyStats;
    private Rigidbody2D enemy_rb;
    private SpriteRenderer spriteRenderer;
    private GameObject playerObj;

    private int expAmount = 100;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy_rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.Find("Player");
        enemyStats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStats.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int dmg)
    {
        enemyStats.SetCurrentHealth(enemyStats.GetCurrentHealth() - dmg);
        StartCoroutine(Flash());
        Knockback();
    }

    private void Die()
    {
        ExperienceManager.Instance.AddExperience(expAmount);
        Destroy(this.gameObject);
    }

    private void Knockback()
    {
        Vector2 knockback_direction = new Vector2(this.transform.position.x - playerObj.transform.position.x, this.transform.position.y - playerObj.transform.position.y);
        knockback_direction = knockback_direction.normalized;
        enemy_rb.AddForce(knockback_direction * 2000f);

    }

    IEnumerator Flash()
    {
        for (int n = 0; n < 1; n++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
