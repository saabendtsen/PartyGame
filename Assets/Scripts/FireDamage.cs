using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{

    public float damage= 5f;
    public bool isInFire = false;

    private IEnumerator coroutine;


    void OnTriggerEnter(Collider collision)
    {
        
        if(collision.gameObject.tag=="Player")
        {
            PlayerHealth playerTarget = collision.transform.gameObject.GetComponent<PlayerHealth>();
            playerTarget.ApplyDamage(damage);
            isInFire = true;
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
            if(collision.gameObject.tag=="Player" && isInFire)
            {
                coroutine = StayInFireDamage(2f);
                StartCoroutine(coroutine);
                PlayerHealth playerTarget = collision.transform.gameObject.GetComponent<PlayerHealth>();
                playerTarget.ApplyDamage(damage);
                isInFire = false;
            }
        
            else if(collision.gameObject.tag=="Enemy" && isInFire)
            {
                coroutine = StayInFireDamage(2f);
                StartCoroutine(coroutine);
                EnemyTarget target = collision.transform.gameObject.GetComponent<EnemyTarget>();
                target.ApplyDamage(damage);
                isInFire = false;
           }
    }

    private IEnumerator StayInFireDamage(float waitTime)
    {
            yield return new WaitForSeconds(waitTime);
            isInFire = true;
            StopCoroutine(coroutine);     
    }

   void OnTriggerExit(Collider collision)
   {
   }
  
}
