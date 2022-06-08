using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnImpact : MonoBehaviour
{
    //public GameObject impactEffect;

    public float damage= 25f;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            EnemyAi target = collision.transform.gameObject.GetComponent<EnemyAi>();
            target.ApplyDamage(damage);
        }
        else if(collision.gameObject.tag=="Player")
        {
            PlayerHealth playerTarget = collision.transform.gameObject.GetComponent<PlayerHealth>();
            playerTarget.ApplyDamage(damage);
        }

        Destroy(gameObject);
    }

}