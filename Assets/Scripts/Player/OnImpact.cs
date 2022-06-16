using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnImpact : MonoBehaviour
{
    //public GameObject impactEffect;

    public float damage;
    private Rigidbody rb;
    private float strayFactor = 50.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            EnemyAi target = collision.transform.gameObject.GetComponent<EnemyAi>();
            target.ApplyDamage(damage);
            rb.velocity = rb.velocity/10;

            //random z and x floats
            float z = Random.Range(-strayFactor, strayFactor);
            float x = Random.Range(-strayFactor, strayFactor);
        
            //rb.AddForce(Vector3.Reflect(rb.velocity, collision.contacts[0].normal));
            rb.AddForce (z,200,x);
        }
        else if(collision.gameObject.tag=="Player")
        {
            PlayerHealth playerTarget = collision.transform.gameObject.GetComponent<PlayerHealth>();
            playerTarget.ApplyDamage(damage);
        }
        else
        {
        rb.velocity = rb.velocity/3;    
        rb.AddForce(Vector3.Reflect(rb.velocity, collision.contacts[0].normal));
        }
        
    }

}