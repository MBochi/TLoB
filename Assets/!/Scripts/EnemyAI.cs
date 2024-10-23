using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    public List<AIBehavior> aIBehavior;

    // Update is called once per frame
    void Update()
    {
        foreach (var item in aIBehavior)
        {
            item?.Run(this);
        }
    }
}
