using System.Collections;
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
    [SerializeField] GameObject chargeBar;
    [SerializeField] GameObject attackPoint;
    [SerializeField] GameObject rotationPoint;
    private Vector2 stickPos;
    private Rigidbody2D rb;
    [SerializeField] private Stats playerStats;
    public static float chargeMax = 1f;

    private bool canAttackX = true;
    private bool canAttackY = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        AttackX();
        AttackY();
    }

    private void AttackY()
    {
        if((Input.GetKey(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.JoystickButton3)) && canAttackY )
        {
            canAttackY = false;
            StartCoroutine(YCooldownTimer(playerStats.GetYCooldown()));
            GameObject explosion = Instantiate(ExplosionPrefab, attackPoint.transform);
            explosion.transform.parent = null;
            explosion.GetComponent<Explosion>().Setup(playerStats.GetAttackDamage() / 2);
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
            StartCoroutine(XCooldownTimer(playerStats.GetXCooldown()));
            if (chargedAttackTimerActive)
            {
                chargeBar.SetActive(false);
                GameObject slash = Instantiate(SlashPrefab, rotationPoint.transform);
                slash.transform.parent = this.transform;

                if (chargedAttackTimer > chargeMax)
                {
                    slash.GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
                    slash.GetComponent<SlashAttack>().Setup(playerStats.GetAttackDamage() * 2);
                }
                else
                {
                    slash.GetComponent<SlashAttack>().Setup(playerStats.GetAttackDamage());
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
}
