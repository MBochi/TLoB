using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private int attackMode = 0; // 0 = melee, 1 = ranged
    private float chargedAttackTimer;
    private bool chargedAttackTimerActive;
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] GameObject SlashPrefab;
    public float attackRadius = 10f;
    public Transform attackPoint;
    [Header("Mouse or XBox Controller")]
    public Boolean switchAimControls = false;
    private bool showGizmo = false;
    private Vector2 stickPos;
    private Vector2 mousePos;
    public Camera cam;
    private Rigidbody2D rb;
    public Transform tf; // rotate object for aiming
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void Aim()
    {
        if (switchAimControls == false) // Keyboard
        {
            // Mouse aiming
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            tf.rotation = Quaternion.Euler(0, 0, angle);
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
            // Controller aiming
            Vector2 idleStickPos = new(Mathf.Round(stickPos.x), Mathf.Round(stickPos.y));
            float stickAngle = Mathf.Atan2(stickPos.y, stickPos.x) * Mathf.Rad2Deg;
            if (idleStickPos != Vector2.zero)
            {
                showGizmo = true;
                tf.rotation = Quaternion.Euler(0, 0, stickAngle * -1);
                chargedAttackTimerActive = true;
            }
            else
            {   
                // if player released stick
                if (chargedAttackTimerActive)
                {
                    if (attackMode == 0) // Melee
                    {
                        GameObject slash = Instantiate(SlashPrefab, tf);
                        slash.transform.SetParent(this.transform);
                        if (chargedAttackTimer > 1f)
                        {
                            slash.GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
                        }
                    }
                    else if (attackMode == 1) // ranged
                    {
                        GameObject arrow = Instantiate(ArrowPrefab, attackPoint);
                        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(attackPoint.position.x - tf.position.x, attackPoint.position.y - tf.position.y) * chargedAttackTimer * 10;
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

    // Update is called once per frame
    void Update()
    {
        Aim();
        Timer();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        stickPos = new Vector2(Input.GetAxisRaw("RightJoyStickHorizontal"), Input.GetAxisRaw("RightJoyStickVertical"));
    }





    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (showGizmo)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius / 10);
        }
        
    }
}
