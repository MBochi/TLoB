using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{ 
    public float camSpeed;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + camSpeed, transform.position.y, transform.position.z);
    }
}
