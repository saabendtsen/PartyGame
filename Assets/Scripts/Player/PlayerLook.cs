using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour{

    public Camera cam;
    Vector2 rotation = Vector2.zero;
    public float speed = 5;

    public void ProcessLook(Vector2 input){
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * speed;
    }
}