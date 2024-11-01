using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    private int attackMode = 0; // 0 = melee, 1 = ranged
    private float chargedAttackTimer;
    private bool chargedAttackTimerActive;
    [SerializeField] private Camera cam;
    [SerializeField] GameObject SlashPrefab;
    [SerializeField] GameObject ExplosionPrefab;
    [SerializeField] GameObject LightningBoltPrefab;
    [SerializeField] GameObject chargeBar;
    [SerializeField] GameObject explosionSpawnPoint;
    [SerializeField] GameObject slashAttackPoint;
    [SerializeField] GameObject rotationPoint;
    private Vector2 stickPos;
    private Rigidbody2D rb;
    [SerializeField] private Stats playerStats;
    public static float chargeMax = 1f;
    private SpriteRenderer spriteRenderer;

    private bool canAttackX = true;
    private bool canAttackY = true;
    private bool canAttackB = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Aim();
        ChargeXTimer();
        UpdateChargeBar();
       
        stickPos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Horizontal"));

    }

    private void Aim()
    {   
        //AttackX();
        //AttackY();
        //AttackB();
        if(playerStats.GetCurrentHealth() == 0)
        {
            Die();
        }
    }

    private void AttackB()
    {
        if((Input.GetKey(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton1)) && canAttackB)
        {
            canAttackB = false;
            GameObject lightningBolt = Instantiate(LightningBoltPrefab, this.transform);
            lightningBolt.GetComponent<LighningtBoltController>().Setup(lightningBolt.GetComponent<WeaponStats>().GetMaxTargets(), 1f, (int)(playerStats.GetComponent<Stats>().GetAttackDamage()/10 + lightningBolt.GetComponent<WeaponStats>().GetAttackDamage()), lightningBolt.GetComponent<WeaponStats>().GetDuration());
            StartCoroutine(BCooldownTimer(lightningBolt.GetComponent<WeaponStats>().GetCooldown()));
        }
    }

    private void AttackY()
    {
        if((Input.GetKey(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.JoystickButton3)) && canAttackY )
        {
            canAttackY = false;

            GameObject explosion = Instantiate(ExplosionPrefab, explosionSpawnPoint.transform);
            explosion.transform.parent = null;

            StartCoroutine(YCooldownTimer(explosion.GetComponent<WeaponStats>().GetCooldown()));

            
            explosion.GetComponent<Explosion>().Setup((int)(playerStats.GetComponent<Stats>().GetAttackDamage()/2 + explosion.GetComponent<WeaponStats>().GetAttackDamage()));
        }
    }

    private void AttackX()
    {
        if((Input.GetKey(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.JoystickButton2)) && canAttackX)
        {
            chargedAttackTimerActive = true;
            chargeBar.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton2) && canAttackX)
        {   
            canAttackX = false;
            
            if (chargedAttackTimerActive)
            {
                chargeBar.SetActive(false);
                GameObject slash = Instantiate(SlashPrefab, rotationPoint.transform);
                slash.transform.parent = this.transform;

                StartCoroutine(XCooldownTimer(slash.GetComponent<WeaponStats>().GetCooldown()));

                int dmg = playerStats.GetComponent<Stats>().GetAttackDamage() + slash.GetComponent<WeaponStats>().GetAttackDamage();

                if (chargedAttackTimer > chargeMax)
                {
                    slash.GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
                    slash.GetComponent<SlashAttack>().Setup(dmg * 2, slashAttackPoint);
                }
                else
                {
                    slash.GetComponent<SlashAttack>().Setup(dmg, slashAttackPoint);
                }
                
            }
            chargedAttackTimerActive = false;
        }
    }

    void ChargeXTimer()
    {
        if (chargedAttackTimerActive)
        {
            chargedAttackTimer += Time.deltaTime;
        }
        else
        {
            chargedAttackTimer = 0;
        }
    }

    void UpdateChargeBar()
    {
        float fillPercent = chargedAttackTimer / chargeMax;
        chargeBar.transform.GetChild(1).GetComponent<Image>().fillAmount = fillPercent;
    }

    public int GetAttackMode()
    {
        return this.attackMode;
    }

    IEnumerator XCooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        canAttackX = true;
    }

    IEnumerator YCooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        canAttackY = true;
    }

    IEnumerator BCooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        canAttackB = true;
    }

    public void TakeDamage(int damage)
    {
        playerStats.SubHealth(damage);
        StartCoroutine(Flash());
    }

    private void Die()
    {
        Destroy(this.gameObject);
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
