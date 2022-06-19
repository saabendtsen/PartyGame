using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject explosion;
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

        //count down lifetime
        maxLifetime -= Time.deltaTime;
        if(maxLifetime <= 0) Explode(); 
    }

    private void Explode()
    {
        
        if(explosion != null) Instantiate(explosion,transform.position, Quaternion.identity);

        //Check for enemy
        Collider[] enemies = Physics.OverlapSphere(transform.position,explosionRange);
        for(int i = 0; i < enemies.Length;i++){
        
             if(enemies[i].gameObject.tag=="Enemy")
             {
                enemies[i].GetComponent<EnemyAi>().ApplyDamage(explosionDamage);
                enemies[i].GetComponent<EnemyAi>().FriendlyFire(explosionForce,transform.position,explosionRange);

             }
        }

      

        Invoke("Delay",0.05f);

    }
     private void Delay()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch)Explode();  
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,explosionRange);
    }

}
