using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    private int attackMode = 0; // 0 = melee, 1 = ranged
    private float chargedAttackTimer;
    private bool chargedAttackTimerActive;
    [SerializeField] private Camera cam;
    [SerializeField] GameObject SlashPrefab;
    [SerializeField] GameObject chargeBar;
    public float attackRadius;
    public Transform attackPoint;
    public Transform attackPointAncor; // rotate object for aiming
    public LayerMask damageableLayers;
    private Vector2 stickPos;
    private Rigidbody2D rb;
    private Stats playerStats;
    public static float chargeMax = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<Stats>();
    }

    void Update()
    {
        Aim();
        Timer();
        UpdateChargeBar();
        stickPos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Aim()
    {
        float stickAngle = Mathf.Atan2(stickPos.y, stickPos.x) * Mathf.Rad2Deg;
        attackPointAncor.rotation = Quaternion.Euler(0, 0, stickAngle);
        
        if(Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            chargedAttackTimerActive = true;
            chargeBar.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton2))
        {   
            if (chargedAttackTimerActive)
            {
                chargeBar.SetActive(false);
                GameObject slash = Instantiate(SlashPrefab, attackPointAncor);
                slash.transform.SetParent(this.transform);
                if (chargedAttackTimer > chargeMax)
                {
                    slash.GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
                }
                // Check if enemy is hit and apply damage
                Collider2D[] hitObjecsts = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, damageableLayers);
                foreach (Collider2D collision_object in hitObjecsts)
                {
                    if(collision_object.gameObject.tag == "Enemy")
                    {
                        if (chargedAttackTimer > chargeMax)
                        {       
                            collision_object.GetComponent<EnemyCombat>().TakeDamage(playerStats.GetAttackDamage() * 2);
                        }
                        else
                        {
                            collision_object.GetComponent<EnemyCombat>().TakeDamage(playerStats.GetAttackDamage());
                        }
                    }
                }
            }
            chargedAttackTimerActive = false;
        }
    }

    void Timer()
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public int GetAttackMode()
    {
        return this.attackMode;
    }
}
