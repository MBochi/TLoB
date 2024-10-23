using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationControllerNew : MonoBehaviour
{
    // Start is called before the first frame update
    private float stickAngle = 0f;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
       //if (Input.GetAxisRaw("Horizontal") != 0f && Input.GetAxisRaw("Horizontal") != 0f)
        //{   
        Vector2 stickPos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        stickAngle = Mathf.Atan2(stickPos.y, stickPos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, stickAngle);
        //}
        //Debug.Log(stickAngle);
    }
}
