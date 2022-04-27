using System.IO.Pipes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteract : MonoBehaviour {

    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private Rigidbody player;
    private PlayerUI playerUI;
    private InputManager inputManager;
    public static PlayerInteract Instance {get;private set;}


    void Start() {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    void Update() {
        playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask)){
            if(hitInfo.collider.GetComponent<Interactable>() != null){
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promtMessage);
                if (inputManager.onFoot.Interact.triggered) {
                    interactable.BaseInteract();
                }
            }
        }
    }

    public void TakeDamage(float explosionForce, Vector3 position,int explosionDamage)
    {
        player.AddForce(explosionForce,explosionForce,explosionForce);
    }
}
