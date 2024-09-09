using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRadius = 10f;
    public Transform attackPoint;
    [Header("Mouse or XBox Controller")]
    public Boolean switchAimControls = false;
    private bool showGizmo = false;
    private bool lockAttackDiretion = false;
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
        if (switchAimControls == false)
        {
            // Mouse aiming
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            tf.rotation = Quaternion.Euler(0, 0, angle);
            showGizmo = true;

        }
        else if (switchAimControls == true) 
        {
            // Controller aiming
            Vector2 idleStickPos = new(Mathf.Round(stickPos.x), Mathf.Round(stickPos.y));
            float stickAngle = Mathf.Atan2(stickPos.y, stickPos.x) * Mathf.Rad2Deg;
            if (idleStickPos != Vector2.zero)
            {
                showGizmo = true;
                if (!lockAttackDiretion)
                {
                    lockAttackDiretion = true;
                    tf.rotation = Quaternion.Euler(0, 0, stickAngle * -1);
                }
            }
            else
            {
                lockAttackDiretion = false;
                showGizmo = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
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
