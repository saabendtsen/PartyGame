using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Move : MonoBehaviour
{

    private enum State
    {
        Roaming,
        ChaseTarget,
        Attacking,
    }
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject playerObj = null;

    private Vector3 targetDestination;
    private Vector3 playerPosition;
    private State state;

    //Health
    private int currentHealth = 100; 
    private bool isDead = false;
    [SerializeField] private HealthDisplay healthDisplay;

    //AttackVar
    public float timeBetweenAttacks;
    private bool alreadAttacked;

    // Start is called before the first frame update
    void Start()
    {

        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        state = State.Roaming;

        agent.stoppingDistance = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //update PlayerPosition
        playerPosition = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z);
        
        //Set Enermy Speed
        anim.SetFloat("Speed", agent.speed);

        //Set chase or roam state
        CheckDistanceToPlayer();

        //update health 
        currentHealth = Mathf.Max(currentHealth, 0);
        if(currentHealth < 0 )
        {
            OnDeath();
        }
        


        switch (state)
        {
            default:
            case State.Roaming:
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    FindNewTargetDestination();
                }
                agent.SetDestination(targetDestination);
                break;

            case State.ChaseTarget:
                agent.SetDestination(playerPosition);
                break;
            case State.Attacking:
                agent.SetDestination(transform.position);
                transform.LookAt(playerPosition);
                if(!alreadAttacked)
                {
                    ///Attach Code here
                    alreadAttacked = true;
                    Invoke(nameof(ResetAttack),timeBetweenAttacks);
                }
                break;


        }
    }

    private void ResetAttack()
     {
         alreadAttacked = false;
     }

    private void FindNewTargetDestination()
    {
        var range = 10f;

        Vector3 randomPoint = transform.position + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
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
        float targetRange = 10f;
        float stopChaseRange = 25f;
        if (Vector3.Distance(transform.position, playerPosition) < targetRange)
        {
            state = State.ChaseTarget;
        }

        if (Vector3.Distance(transform.position, playerPosition) > stopChaseRange)
        {
            state = State.Roaming;
        }
    }

    private void OnDeath()
    {
        isDead = true;
        var ragdoll = GetComponent<RagdollHandler>();
        ragdoll.GoRagdoll(true);
        GetComponent<NavMeshAgent>().isStopped = true;

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
