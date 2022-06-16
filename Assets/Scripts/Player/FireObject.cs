using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    
    public GameObject Bullet;

    public float Force,timeBetweenShooting,strayFactor;

    private bool allowInvoke,readyToShoot;

    // Start is called before the first frame update
    void Start()
    {
        allowInvoke = true;
        readyToShoot = true;
        strayFactor = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && readyToShoot)
        {
            Shoot();
        }
    }



    void Shoot()
    {
            readyToShoot = false;
            GameObject BulletHolder;       

            BulletHolder = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = BulletHolder.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(transform.forward * Force, ForceMode.Impulse);
            Destroy(BulletHolder, 2.0f);
            if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }


    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}
