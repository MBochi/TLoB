using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponStats", menuName = "WeaponStats")]
public class WeaponStatsSO : ScriptableObject
{
    public int damage;
    public float cooldown;
}
