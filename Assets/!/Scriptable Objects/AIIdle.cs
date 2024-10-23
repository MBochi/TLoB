using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/AIIdle", fileName = "newAIIdle")]
public class AIIdle : AIBehavior
{
    public override void Run(EnemyAI enemyAI)
    {
        if (enemyAI != null)
        {
            if (enemyAI.gameObject.TryGetComponent<EnemyMovement>(out var movement))
            {
                movement.Idle();
            }
        }
    }
}
