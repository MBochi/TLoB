using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int movementSpeed;
    public int health;
    public int defense;
    public int attackDamage;
    public int attackSpeed;

    #region Getters & Setters
    public int GetHealth()
    {
        return this.health;
    }
    public void SetHealth(int health)
    {
        this.health = health;
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
}
#endregion