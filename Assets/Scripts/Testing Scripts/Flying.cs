using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    float moveSpeed = 5f;
    Rigidbody rb;

    Vector2 movement;

    void Update(){
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate(){
        
    }
}
