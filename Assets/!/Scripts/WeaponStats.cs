using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private WeaponStatsSO weaponStatsSO;


    public int GetAttackDamage()
    {
        return weaponStatsSO.damage;
    }

    public float GetCooldown()
    {
        return weaponStatsSO.cooldown;
    }
}
