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
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] GameObject SlashPrefab;
    [SerializeField] GameObject chargeBar;
    public float attackRadius;
    public Transform attackPoint;
    public Transform attackPointAncor; // rotate object for aiming
    public LayerMask damageableLayers;
    private bool showGizmo = false;
    [Header("Mouse or XBox Controller")]
    public Boolean switchAimControls = false;
    private Vector2 stickPos;
    private Vector2 mousePos;
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
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        stickPos = new Vector2(Input.GetAxisRaw("RightJoyStickHorizontal"), Input.GetAxisRaw("RightJoyStickVertical"));
    }

    private void Aim()
    {
        if (switchAimControls == false) // Keyboard
        {
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            attackPointAncor.rotation = Quaternion.Euler(0, 0, angle);
            showGizmo = true;

        }
        else if (switchAimControls == true) //Controller
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton3) && !chargedAttackTimerActive)
            {
                attackMode += 1;
                if (attackMode > 1)
                {
                    attackMode = 0;
                }
            }
            Vector2 idleStickPos = new(Mathf.Round(stickPos.x), Mathf.Round(stickPos.y));
            float stickAngle = Mathf.Atan2(stickPos.y, stickPos.x) * Mathf.Rad2Deg;
            if (idleStickPos != Vector2.zero) // Player aimes with stick
            {
                showGizmo = true;
                attackPointAncor.rotation = Quaternion.Euler(0, 0, stickAngle * -1);
                chargedAttackTimerActive = true;
                chargeBar.SetActive(true);
            }
            else
            {   
                // if player released stick
                if (chargedAttackTimerActive)
                {
                    chargeBar.SetActive(false);
                    if (attackMode == 0) // Melee
                    {
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
                    else if (attackMode == 1) // ranged
                    {
                        GameObject arrow = Instantiate(ArrowPrefab, attackPoint);
                        if (chargedAttackTimer > chargeMax)
                        {
                            chargedAttackTimer = chargeMax;
                        } 
                        arrow.GetComponent<Arrow>().SetProjectileDamage(playerStats.GetAttackDamage());
                        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(attackPoint.position.x - attackPointAncor.position.x, attackPoint.position.y - attackPointAncor.position.y) * chargedAttackTimer * 10;
                        arrow.transform.parent = null;
                    }
                }
                chargedAttackTimerActive = false;
                showGizmo = false;
            }
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
        if (showGizmo)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
        
    }

    public int GetAttackMode()
    {
        return this.attackMode;
    }
}
