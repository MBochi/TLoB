using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent) {}
}