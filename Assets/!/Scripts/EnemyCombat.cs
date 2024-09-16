using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // Start is called before the first frame update
    private Stats enemyStats;
    void Start()
    {
        enemyStats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStats.GetHealth() <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int dmg)
    {
        enemyStats.SubHealth(dmg);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
