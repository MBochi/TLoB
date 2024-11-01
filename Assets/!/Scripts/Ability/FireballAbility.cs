using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Fireball", fileName = "FireballAbility")]
public class FireballAbility : Ability
{
    [SerializeField] private GameObject prefab;

    public override void Activate(GameObject parent)
    {

        Stats playerStats = parent.GetComponent<Stats>();
        Transform rotationAnchor = parent.transform.Find("RotationAnchor");
        Transform spawnPoint = rotationAnchor.Find("FireballSpawnPoint");
        

        if (spawnPoint == null)
        {
            Debug.LogWarning("FireballSpawnPoint not found in " + parent.name);
        }

        if (rotationAnchor == null)
        {
            Debug.LogWarning("RotationAnchor not found in " + parent.name);
        }

        GameObject explosion = Instantiate(prefab, spawnPoint.position, rotationAnchor.rotation);
        explosion.transform.parent = null;

        explosion.GetComponent<Explosion>().Setup((int)(playerStats.GetAttackDamage()/2 + damage));
    }
}