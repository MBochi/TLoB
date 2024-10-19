using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private StatsSO statsSO;
    [SerializeField] private int movementSpeed;
    [SerializeField] private int currentHealth;

    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }
    private void HandleExperienceChange(int newExperience)
    {
        statsSO.currentExperience += newExperience;
        if (statsSO.currentExperience >= statsSO.maxExperience) 
        {
            LevelUp();
        }
    }

    private void LevelUp() 
    {
        currentHealth = statsSO.maxHealth;
        statsSO.currentLevel++;
        statsSO.currentPoints++;
        statsSO.currentExperience = 0;
        statsSO.maxExperience += 100;
    }
    #region Getters & Setters
    public int GetCurrentHealth()
    {
        return this.currentHealth;
    }
    public void SetCurrentHealth(int amount)
    {
        this.currentHealth = amount;
    }
    public void AddHealth(int amount)
    {
        this.currentHealth += amount;
        if (this.currentHealth > statsSO.maxHealth)
        {
            this.currentHealth = statsSO.maxHealth;
        }
    }
    public void SubHealth(int amount)
    {
        this.currentHealth -= amount;
        if (this.currentHealth < 0)
        {
            this.currentHealth = 0;
        }
    }
    public int GetMaxHealth()
    {
        return statsSO.maxHealth;
    }
    public void SetMaxHealth(int maxHealth)
    {
        statsSO.maxHealth = maxHealth;
    }
    public int GetCurrentExperience()
    {
        return statsSO.currentExperience;
    }
    public void SetCurrentExperience(int currentExp)
    {
        statsSO.currentExperience = currentExp;
    }
    public int GetMaxExperience()
    {
        return statsSO.maxExperience;
    }
    public void SetMaxExperience(int maxExp)
    {
        statsSO.maxExperience = maxExp;
    }
    public int GetCurrentLevel()
    {
        return statsSO.currentLevel;
    }
    public void SetCurrentLevel(int level)
    {
        statsSO.currentLevel = level;
    }
    public int GetAvailablePoints()
    {
        return statsSO.currentPoints;
    }
    public void SetAvailablePoints(int level)
    {
        statsSO.currentPoints = level;
    }
    public int GetMovementSpeed()
    {
        return this.movementSpeed;
    }
    public void SetMovementSpeed(int movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
    public int GetDefense()
    {
        return statsSO.defense;
    }
    public void SetDefense(int defense)
    {
        statsSO.defense = defense;
    }
    public int GetAttackDamage()
    {
        return statsSO.attackDamage;
    }
    public void SetAttackDamage(int attackDamage)
    {
        statsSO.attackDamage = attackDamage;
    }
    public int GetAttackSpeed()
    {
        return statsSO.attackSpeed;
    }
    public void SetAttackSpeed(int attackSpeed)
    {
        statsSO.attackSpeed = attackSpeed;
    }
    public float GetAttackRadius()
    {
        return statsSO.attackRadius;
    }
    public void SetAttackRadius(float radius)
    {
        statsSO.attackRadius = radius;
    }

    public float GetXCooldown()
    {
        return statsSO.XCooldown;
    }
    public void SetXCooldown(float cooldown)
    {
        statsSO.XCooldown = cooldown;
    }

    public float GetYCooldown()
    {
        return statsSO.YCooldown;
    }
    public void SetYCooldown(float cooldown)
    {
        statsSO.YCooldown = cooldown;
    }

}
#endregion