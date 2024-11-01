using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float activeTime;
    public int damage;

    public virtual void Activate(GameObject parent) {}
}