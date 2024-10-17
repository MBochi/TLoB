using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int movementSpeed;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentExperience;
    [SerializeField] private int maxExperience;
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentPoints;
    [SerializeField] private int defense;
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    [SerializeField] private float attackRadius;

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
        currentExperience += newExperience;
        if (currentExperience >= maxExperience) 
        {
            LevelUp();
        }
    }

    private void LevelUp() 
    {
        health = maxHealth;
        currentLevel++;
        currentPoints++;
        currentExperience = 0;
        maxExperience += 100;
    }
    #region Getters & Setters
    public int GetHealth()
    {
        return this.health;
    }
    public void SetHealth(int health)
    {
        this.health = health;
    }
    public void AddHealth(int amount)
    {
        this.health += amount;
        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }
    }
    public void SubHealth(int amount)
    {
        this.health -= amount;
        if (this.health < 0)
        {
            this.health = 0;
        }
    }
    public int GetMaxHealth()
    {
        return this.maxHealth;
    }
    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }
    public int GetCurrentExperience()
    {
        return this.currentExperience;
    }
    public void SetCurrentExperience(int currentExp)
    {
        this.currentExperience = currentExp;
    }
    public int GetMaxExperience()
    {
        return this.maxExperience;
    }
    public void SetMaxExperience(int maxExp)
    {
        this.maxExperience = maxExp;
    }
    public int GetCurrentLevel()
    {
        return this.currentLevel;
    }
    public void SetCurrentLevel(int level)
    {
        this.currentLevel = level;
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
        return this.defense;
    }
    public void SetDefense(int defense)
    {
        this.defense = defense;
    }
    public int GetAttackDamage()
    {
        return this.attackDamage;
    }
    public void SetAttackDamage(int attackDamage)
    {
        this.attackDamage = attackDamage;
    }
    public int GetAttackSpeed()
    {
        return this.attackSpeed;
    }
    public void SetAttackSpeed(int attackSpeed)
    {
        this.attackSpeed = attackSpeed;
    }
    public float GetAttakRadius()
    {
        return this.attackRadius;
    }
    public void SetAttackRadius(float radius)
    {
        this.attackRadius = radius;
    }
}
#endregion