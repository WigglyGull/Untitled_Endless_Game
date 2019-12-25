using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour{
    public float moveSpeed = 6;
    Vector3 velocity;
    float gravity = -20;

    Controller2d controller;

    void Start(){
        controller = GetComponent<Controller2d>();
    }

    void Update(){
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        velocity.x = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
