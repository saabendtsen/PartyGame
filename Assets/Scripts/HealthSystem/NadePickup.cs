using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NadePickup : MonoBehaviour {

    public int amount = 5;
    public float respawnTimer = 10f;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
          ThrowWeapon nade = other.GetComponent<ThrowWeapon>();
          nade.NadePickup(amount);
          gameObject.SetActive(false);
          Invoke(nameof(RespawnPickup), respawnTimer);
            
        }
    }
    private void RespawnPickup(){
        gameObject.SetActive(true);
    }

}
