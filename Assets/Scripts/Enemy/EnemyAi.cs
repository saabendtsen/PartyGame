using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
        AttackPlayer
    }

    private NavMeshAgent agent;

    [SerializeField]
    public Rigidbody rigidbody;

    private Animator anim;

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

    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
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

        //Set Enermy Speed
        anim.SetFloat("Speed", agent.speed);

        //Set chase or roam
        CheckDistanceToPlayer();

        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;
            case State.ChaseTarget:
                agent.SetDestination(playerPosition);
                break;
            case State.AttackPlayer:
                AttackPlayer();
                break;
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
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)
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

    public void FriendlyFire()
    {
        Debug.Log("Took dmg");
    }
}
