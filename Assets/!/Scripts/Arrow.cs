using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    private int projectileDamage = 0;
    void Start()
    {
        
    }

    void OnBecameInvisible() {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetProjectileDamage(int dmg)
    {
        this.projectileDamage = dmg;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
    }
        
    
}
