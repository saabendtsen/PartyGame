using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float health = 100f;

    public void ApplyDamage(float amount)
    {
        Debug.Log("Player take Damage");
        health -= amount;
        if(health <= 0)
        {
        Debug.Log("YOU DIED! NOOB");
        }
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
