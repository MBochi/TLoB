using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector2 stickPos;
    private float timer = 0f;
    private int damage;
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
        Debug.Log(timer);
        if (timer >= .9f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(10);
        }
    }
}
