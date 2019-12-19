using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2d))]
public class NewPlayer : MonoBehaviour{
    Vector3 velocity;
    float gravity = -20;

    Controller2d controller;

    void Start(){
        controller = GetComponent<Controller2d>();
    }

    void Update(){
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
