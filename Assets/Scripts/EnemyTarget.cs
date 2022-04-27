using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{

    public float health = 100f;
   // public GameObject deadBody;

   // private bool created = false;

    public void ApplyDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
           //   if(!created)
           // {
                //Instantiate(deadBody, transform.position, transform.localRotation);
            //    created = true;
           // }
        Debug.Log("Dead");
        Die();
        }
    }
    void Die()
    {
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
