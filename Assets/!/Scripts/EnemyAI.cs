using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AIBehavior aIBehavior;

    // Update is called once per frame
    void Update()
    {
        aIBehavior.Run(this);
    }
}
