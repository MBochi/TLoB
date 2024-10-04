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
        AttackX();
        AttackY();
    }

    private void AttackY()
    {
        if(Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            GameObject explosion = Instantiate(ExplosionPrefab, attackPoint.transform);
            explosion.transform.parent = null;
            explosion.GetComponent<Explosion>().Setup(10);
        }
    }

    private void AttackX()
    {
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
                GameObject slash = Instantiate(SlashPrefab, this.transform);

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

    public int GetAttackMode()
    {
        return this.attackMode;
    }
}
