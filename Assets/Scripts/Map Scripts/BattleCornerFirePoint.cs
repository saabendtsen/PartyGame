using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCornerFirePoint : MonoBehaviour
{
    public GameObject Bullet;
    
    public float Force = 2000f;

    public bool timeToShoot = false;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = timeToShooting(2f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToShoot)
        {
            GameObject BulletHolder;
            BulletHolder = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
            BulletHolder.transform.Rotate(Vector3.left * 90);

            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = BulletHolder.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(transform.forward * Force);
            timeToShoot = false;
            coroutine = timeToShooting(2f);
            StartCoroutine(coroutine);
            Destroy(BulletHolder, 1f);
        }
    }

    private IEnumerator timeToShooting(float waitTime)
    {
            yield return new WaitForSeconds(waitTime);
            timeToShoot = true;
            StopCoroutine(coroutine);       
    }
}
