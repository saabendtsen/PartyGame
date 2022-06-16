using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public enum State
    {
        Roaming,
        ChaseTarget,
        AttackPlayer
    }

    private NavMeshAgent agent;

    public float health = 100f;

    private bool isFlying;

    [SerializeField]
    public Rigidbody rigidbody;

    private GameObject Ground;

    private GameObject playerObj = null;

    [SerializeField]
    private ProjectileGun projectileGun;

    private Vector3 targetDestination;

    private Vector3 startPosition;

    private Vector3 currentPosition;

    private Vector3 playerPosition;

    private State state;
   
    private float attackTimer;

    public float attackRange;

    public float chaseRange;

    public float stopChaseRange;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }
        if (Ground == null)
        {
            Ground = GameObject.Find("Ground");
        }

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        state = State.Roaming;
        startPosition =
            new Vector3(transform.position.x,
                transform.position.y,
                transform.position.z);

        agent.stoppingDistance = 2;
            }

    // Update is called once per frame
    void Update()
    {
        //update PlayerPosition
        playerPosition =
            new Vector3(playerObj.transform.position.x,
                playerObj.transform.position.y,
                playerObj.transform.position.z);
        currentPosition =
            new Vector3(transform.position.x,
                transform.position.y,
                transform.position.z);

        //Set chase or roam
        if(agent.enabled)
        {
        CheckDistanceToPlayer();
        switch (state)
        {
            default:
            case State.Roaming:
            anim.SetBool("roaming", true);
            anim.SetBool("chase", false);
            anim.SetBool("attack", false);
                Roaming();
                break;
            case State.ChaseTarget:
            anim.SetBool("chase", true);
            anim.SetBool("roaming", false);
            anim.SetBool("attack", false);
                agent.SetDestination(playerPosition);
                break;
            case State.AttackPlayer:
            anim.SetBool("attack", true);
            anim.SetBool("roaming", false);
            anim.SetBool("chase", false);
                AttackPlayer();
                break;
        }
        }

         if(transform.position.y >4)
        {
            isFlying = true;
        }

        if(transform.position.y <3.7 && isFlying)
        {
            ResetAgent();
        }
    }

    private void ChasePlayer(){
        
        if(Vector3.Distance(currentPosition, playerPosition)>attackRange)
        {
            agent.SetDestination(playerPosition);
        }
        if(Vector3.Distance(currentPosition, playerPosition)<attackRange)
        {
            agent.SetDestination(currentPosition);
            LookAtPlayer();
        }
    }

    private void Roaming()
    {
        if(agent.enabled)
        {
    
        if (Vector3.Distance(currentPosition, currentPosition) < 30)
        {
            agent.SetDestination (startPosition);
        }
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            FindNewTargetDestination();
        }
        if(agent)
        agent.SetDestination(targetDestination);
        }
 
    }

    public void addHealth()
    {
        health = 125f;
    }

    private void AttackPlayer()
    {
        LookAtPlayer();
        projectileGun.MyInput(playerPosition);
        agent.SetDestination(currentPosition);
    }

    private void FindNewTargetDestination()
    {
        var range = 10f;

        Vector3 randomPoint =
            transform.position + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 3.0f, NavMesh.AllAreas)
        )
        {
            targetDestination = hit.position;
        }
        else
        {
            FindNewTargetDestination();
        }
    }

    private void CheckDistanceToPlayer()
    {
        //If Plaer are within ChaseTarget
        if (Vector3.Distance(currentPosition, playerPosition) < chaseRange)
        {
            state = State.ChaseTarget;
        }
        if (Vector3.Distance(currentPosition, playerPosition) < attackRange)
        {
            state = State.AttackPlayer;
        }
        if (Vector3.Distance(currentPosition, playerPosition) > stopChaseRange)
        {
            state = State.Roaming;
        }
    }

    private void LookAtPlayer()
    {
        Vector3 relativePos = playerObj.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion current = transform.localRotation;
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime                              
            * 5);
    }

    public void FriendlyFire(float explosionForce, Vector3 explosionPosition,float explosionRadius)
    {

        agent.enabled = false;
        rigidbody.isKinematic = false;
        rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 10.0F, ForceMode.Impulse);

    }

    void ResetAgent()
    {
        agent.enabled = true;
        rigidbody.isKinematic = true;
    }

    public void ApplyDamage(float amount)
    {
        Debug.Log("Took dmg");
        health -= amount;
        if(health <= 0)
        {
        Die();
        }
    }

    void Die()
    {
        Ground.GetComponent<SpawnHandler>().EnemyKilled();
        Destroy(gameObject);
    }
}
