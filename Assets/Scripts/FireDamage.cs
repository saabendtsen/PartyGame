using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{

    public float damage= 10f;


    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            PlayerHealth playerTarget = collision.transform.gameObject.GetComponent<PlayerHealth>();
            playerTarget.ApplyDamage(damage);
            Debug.Log("Player enter fire");
        }
        else if(collision.gameObject.tag=="Enemy")
        {
            EnemyTarget target = collision.transform.gameObject.GetComponent<EnemyTarget>();
            target.ApplyDamage(damage);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            PlayerHealth playerTarget = collision.transform.gameObject.GetComponent<PlayerHealth>();
            damage += 2f;
            playerTarget.ApplyDamage(damage);
            
            Debug.Log("Player Took damage = " + damage);
        }
        else if(collision.gameObject.tag=="Enemy")
        {
            EnemyTarget target = collision.transform.gameObject.GetComponent<EnemyTarget>();
            damage += 2f;
            target.ApplyDamage(damage);
        }
    }
   void OnTriggerExit(Collider collision)
   {
       if(collision.gameObject.tag=="Player")
        {   
            damage = 10f;   
            Debug.Log("Player Exits fire");
        }
        else if(collision.gameObject.tag=="Enemy")
        {
            EnemyTarget target = collision.transform.gameObject.GetComponent<EnemyTarget>();
            target.ApplyDamage(damage);
        }
   }
}
