using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    
    public PlayerInput.OnFootActions onFoot; 
    private PlayerInput playerInput; 
    private PlayerMotorics motorics;
    private PlayerLook look;

    void Awake(){
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motorics = GetComponent<PlayerMotorics>();
        look = GetComponent<PlayerLook>();
        // hver gang vi kalder jump laver vi en callback til motor jump functionen
        onFoot.Jump.performed += ctx => motorics.Jump();
        onFoot.Crouch.performed += ctx => motorics.Crouch();
        onFoot.Sprint.performed += ctx => motorics.Sprint();
    }

    void FixedUpdate() {
        motorics.ProcessMovement(onFoot.Movement.ReadValue<Vector2>());
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void LateUpdate() {
        //look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable() {
        onFoot.Enable();
    }

    private void OnDisable() {
        onFoot.Disable();
    }
}

