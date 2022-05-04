using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    
    public GameObject Bullet;
    
    public float Force = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject BulletHolder;
            BulletHolder = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
            BulletHolder.transform.Rotate(Vector3.left * 90);

            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = BulletHolder.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(transform.forward * Force);

            Destroy(BulletHolder, 2.0f);
        }
    }
}
