using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats")]
public class StatsSO : ScriptableObject
{
    public int maxHealth;
    public int currentExperience;
    public int maxExperience;
    public int currentLevel;
    public int currentPoints;
    public int defense;
    public int attackDamage;
    public int attackSpeed;
    public float attackRadius;
    public float XCooldown;
    public float YCooldown;
}


