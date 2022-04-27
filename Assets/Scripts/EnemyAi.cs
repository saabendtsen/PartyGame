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
        AttackPlayer,
    }
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject playerObj = null;

    [SerializeField]private ProjectileGun projectileGun;

    private Vector3 targetDestination;
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 playerPosition;
    private State state;
    private float attackTimer;

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
        startPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        

        agent.stoppingDistance = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //update PlayerPosition
        playerPosition = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, playerObj.transform.position.z);
        currentPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);

        
        //Set Enermy Speed
        anim.SetFloat("Speed", agent.speed);

        //Set chase or roam
        CheckDistanceToPlayer();

        switch (state)
        {
            default:
            case State.Roaming:
                if (Vector3.Distance(currentPosition, currentPosition) < 50)
                {
                    agent.SetDestination(startPosition);
                }
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    FindNewTargetDestination();
                }
                if(agent)
                agent.SetDestination(targetDestination);
                break;

            case State.ChaseTarget:
                agent.SetDestination(playerPosition);
                break;
            case State.AttackPlayer:
                AttackPlayer();
                break;

        }
    }

    private void AttackPlayer()
    {
        projectileGun.MyInput(playerPosition);
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
        float attackRange = 25f;
        attackTimer -= Time.deltaTime;
        state = State.Roaming;
        if (Vector3.Distance(transform.position, playerPosition) < targetRange)
        {
            state = State.ChaseTarget;
        }
        if (Vector3.Distance(transform.position, playerPosition) < attackRange && attackTimer <= 0)
        {
            state = State.AttackPlayer;
            attackTimer = 4f;
            agent.isStopped = true;
            Invoke("StartAgent",1f);
        }

    }

    private void StartAgent(){
        agent.isStopped = false;
    }
}
