using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/AIChase", fileName = "newAIChase")]
public class AIChase : AIBehavior
{
    [SerializeField] private string targetTag;
    public override void Run(EnemyAI enemyAI)
    {
        if (enemyAI != null) 
        {
            GameObject target = GameObject.FindGameObjectWithTag(targetTag);
            
            if (enemyAI.gameObject.TryGetComponent<EnemyMovement>(out var movement)) 
            {
                movement.MoveTowardsTarget(target.transform.position);
            }
        } 
    }
}
