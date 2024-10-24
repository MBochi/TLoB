using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BurnEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem burnParticles;

    private EnemyCombat enemyCombat;
    private float maxBurnTime;
    private float burnSpeed;
    private int burnDamage;
    private float totalTimer = 0f;
    private float burnTimer = 0f;
    private bool isActive = true;
    void Start()
    {
        burnParticles = GetComponent<ParticleSystem>();
        burnParticles.Play();
    }

    public void Setup(int damage, float maxTime, float speed, EnemyCombat combat)
    {
        this.burnDamage = damage;
        this.maxBurnTime = maxTime;
        this.burnSpeed = speed;
        this.enemyCombat = combat;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            this.totalTimer += Time.deltaTime;
            this.burnTimer += Time.deltaTime;

            if (this.burnTimer > this.burnSpeed)
            {
                this.burnTimer = 0f;
                enemyCombat.TakeDamage(this.burnDamage);
            }
            

            if(this.totalTimer >= this.maxBurnTime)
            {
                burnParticles.Stop();
                StartCoroutine(Delete());
            }
        }
    }

    IEnumerator Delete()
    {
        this.isActive = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}
