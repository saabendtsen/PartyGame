using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float amount = 50;
    public float respawnTimer = 10f;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
          PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health.health < 100f) {
            health.RestoreHealth(amount);
            gameObject.SetActive(false);
            Invoke(nameof(RespawnPickup), respawnTimer);
            }
        }
    }
    private void RespawnPickup(){
        gameObject.SetActive(true);
    }

}
