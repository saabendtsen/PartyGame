using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsEnemy;


    //stats
    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;


    //Damage 
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;


    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        //when to explode
        if(collisions > maxCollisions) Explode();

        //count down lifetime
        maxLifetime -= Time.deltaTime;
        if(maxLifetime <= 0) Explode();
    }

    private void Explode()
    {
        if(explosion != null) Instantiate(explosion,transform.position, Quaternion.identity);

        //Check for enemy
        Collider[] enemies = Physics.OverlapSphere(transform.position,explosionRange,whatIsEnemy);
        for(int i = 0; i < enemies.Length;i++){
            //This shit not working -.-

            //apply knowback on enemy
            //enemies[i].GetComponent<EnemyAi>().FriendlyFire(explosionForce,transform.position,explosionRange);
        }

        //check for player
        Collider[] player = Physics.OverlapSphere(transform.position,explosionRange,whatIsPlayer);
        
        for(int i = 0; i < enemies.Length;i++){
             //This shit not working -.-

             //give player damage
            //player[i].GetComponent<PlayerInteract>().TakeDamage(explosionForce,transform.position,explosionDamage);
            
        }

        Invoke("Delay",0.05f);

    }
     private void Delay()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision) {
        collisions++;

        if (collision.collider.CompareTag("Player") && explodeOnTouch) Explode();
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();

    }

   






    private void Setup()
    {
        //Create physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        // Assign material to colider
        GetComponent<SphereCollider>().material = physics_mat;

        //set Gravity
        rb.useGravity = useGravity;

        
    }

   private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRange);
    }

}
