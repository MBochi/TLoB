using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Dash", fileName = "DashAbility")]
public class DashAbility : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        Rigidbody2D rigidbody = parent.GetComponent<Rigidbody2D>();
        
        //Does not work right now
        rigidbody.velocity = movement.GetMovementInput().normalized * dashVelocity;
    }
}