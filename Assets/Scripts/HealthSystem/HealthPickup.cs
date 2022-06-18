using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float amount = 50;

    private void OnTriggerEnter(Collider other) {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health.health < 100f) {
            health.RestoreHealth(amount);
            Destroy(gameObject);
        }
    }
}
