using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{

    public float health = 100f;
    public GameObject arena;
    SpawnHandler spawner;

    public void ApplyDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
        Die();
        }
    }

    void Die()
    {
        arena.GetComponent<SpawnHandler>().EnemyKilled();
        Destroy(gameObject);
    
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
