using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    public float attackRadius = 10f;
    public Transform tf;
  
    
    void Start()
    {

    }

    
    void Update()
    {

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(tf.position, attackRadius / 10);
    }
}
