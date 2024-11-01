using UnityEditor;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    #region Ability 1
    [Header("Ability 1")]
    public Ability ability1;
    private float ability1CooldownTime;
    private float ability1ActiveTime;
    AbilityState ability1State = AbilityState.ready;
    public KeyCode ability1Key;
    #endregion

    #region Ability 2
    [Header("Ability 2")]
    public Ability ability2;
    private float ability2CooldownTime;
    private float ability2ActiveTime;
    AbilityState ability2State = AbilityState.ready;
    public KeyCode ability2Key;
    #endregion

    #region Ability 3
    [Header("Ability 3")]
    public Ability ability3;
    private float ability3CooldownTime;
    private float ability3ActiveTime;
    AbilityState ability3State = AbilityState.ready;
    public KeyCode ability3Key;
    #endregion

    #region Ability 2
    [Header("Ability 2")]
    public Ability ability4;
    private float ability4CooldownTime;
    private float ability4ActiveTime;
    AbilityState ability4State = AbilityState.ready;
    public KeyCode ability4Key;
    #endregion

    
    void Update()
    {
        HandleAbility1();
        HandleAbility2();
        HandleAbility3();
        HandleAbility4();
    }

    private void HandleAbility1()
    {
        switch (ability1State)
        {
            case AbilityState.ready:
                if(Input.GetKeyDown(ability1Key))
                {
                    ability1.Activate(gameObject);
                    ability1State = AbilityState.active;
                    ability1ActiveTime = ability1.activeTime;
                }
                break;
            case AbilityState.active:
                if (ability1ActiveTime > 0)
                {
                    ability1ActiveTime -= Time.deltaTime;
                }
                else
                {
                    ability1State = AbilityState.cooldown;
                    ability1CooldownTime = ability1.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (ability1CooldownTime > 0)
                {
                    ability1CooldownTime -= Time.deltaTime;
                }
                else
                {
                    ability1State = AbilityState.ready;
                }
                break;
        }
    }

    private void HandleAbility2()
    {
        switch (ability2State)
        {
            case AbilityState.ready:
                if(Input.GetKeyDown(ability2Key))
                {
                    ability2.Activate(gameObject);
                    ability2State = AbilityState.active;
                    ability2ActiveTime = ability2.activeTime;
                }
                break;
            case AbilityState.active:
                if (ability2ActiveTime > 0)
                {
                    ability2ActiveTime -= Time.deltaTime;
                }
                else
                {
                    ability2State = AbilityState.cooldown;
                    ability2CooldownTime = ability2.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (ability2CooldownTime > 0)
                {
                    ability2CooldownTime -= Time.deltaTime;
                }
                else
                {
                    ability2State = AbilityState.ready;
                }
                break;
        }
    }

    private void HandleAbility3()
    {
        switch (ability3State)
        {
            case AbilityState.ready:
                if(Input.GetKeyDown(ability3Key))
                {
                    ability3.Activate(gameObject);
                    ability3State = AbilityState.active;
                    ability3ActiveTime = ability3.activeTime;
                }
                break;
            case AbilityState.active:
                if (ability3ActiveTime > 0)
                {
                    ability3ActiveTime -= Time.deltaTime;
                }
                else
                {
                    ability3State = AbilityState.cooldown;
                    ability3CooldownTime = ability3.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (ability3CooldownTime > 0)
                {
                    ability3CooldownTime -= Time.deltaTime;
                }
                else
                {
                    ability3State = AbilityState.ready;
                }
                break;
        }
    }

    private void HandleAbility4()
    {
        switch (ability4State)
        {
            case AbilityState.ready:
                if(Input.GetKeyDown(ability4Key))
                {
                    ability4.Activate(gameObject);
                    ability4State = AbilityState.active;
                    ability4ActiveTime = ability4.activeTime;
                }
                break;
            case AbilityState.active:
                if (ability4ActiveTime > 0)
                {
                    ability4ActiveTime -= Time.deltaTime;
                }
                else
                {
                    ability4State = AbilityState.cooldown;
                    ability4CooldownTime = ability4.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (ability4CooldownTime > 0)
                {
                    ability4CooldownTime -= Time.deltaTime;
                }
                else
                {
                    ability4State = AbilityState.ready;
                }
                break;
        }
    }

}