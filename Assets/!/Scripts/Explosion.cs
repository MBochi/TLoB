using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float timer = 0f;
    private int damage;
    [SerializeField] GameObject BurnEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup(int damage)
    {
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= .9f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(this.damage, true);

            bool isBurning = false;
            foreach (Transform child in collision.transform)
            {
                if(child.gameObject.tag == "StatusEffect")
                {
                    isBurning = true;
                }
            }

            if (!isBurning)
            {
                GameObject burn = Instantiate(BurnEffectPrefab, collision.transform);
                burn.GetComponent<BurnEffect>().Setup(5, 5, 1, collision.gameObject.GetComponent<EnemyCombat>()); //Stats noch hardcoded
            }
            
        }
    }
}
