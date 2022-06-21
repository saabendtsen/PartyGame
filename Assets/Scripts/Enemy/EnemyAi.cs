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
    public new Rigidbody rigidbody;

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

    AudioSource audioData;

    public GameObject blood;
    public GameObject blood2;


    // Start is called before the first frame update
    void Start()
    {

        audioData = GetComponent<AudioSource>();

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
        //Debug.Log("Agent enabled?" + agent.enabled);
        //Set chase or roam
        if (agent.enabled)
        {
            CheckDistanceToPlayer();
            switch (state)
            {
                default:
                case State.Roaming:
                    Roaming();
                    break;
                case State.ChaseTarget:
                    if (agent.enabled) agent.SetDestination(playerPosition);
                    anim.SetBool("chase", true);
                    anim.SetBool("roaming", false);
                    anim.SetBool("attack", false);
                    break;
                case State.AttackPlayer:
                    AttackPlayer();
                    break;
            }
        }

        //if y is below 0 call Die()
        if (transform.position.y <= 0)
        {
            Die();
           
        }

    }


    private void ChasePlayer()
    {

        if (Vector3.Distance(currentPosition, playerPosition) > attackRange)
        {
            agent.SetDestination(playerPosition);
        }
        if (Vector3.Distance(currentPosition, playerPosition) < attackRange)
        {
            agent.SetDestination(currentPosition);
            LookAtPlayer();
        }
    }

    private void Roaming()
    {
        anim.SetBool("roaming", true);
        anim.SetBool("chase", false);
        anim.SetBool("attack", false);
        if (agent.enabled)
        {

            if (Vector3.Distance(currentPosition, currentPosition) < 30)
            {
                agent.SetDestination(startPosition);
            }
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                FindNewTargetDestination();
            }
            if (agent)
                agent.SetDestination(targetDestination);
        }



    private void AttackPlayer()
    {
        LookAtPlayer();
        projectileGun.MyInput(playerPosition);
        agent.SetDestination(currentPosition);
        anim.SetBool("attack", true);
        anim.SetBool("roaming", false);
        anim.SetBool("chase", false);
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
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * 5);
    }

    public void FriendlyFire(float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {

        agent.enabled = false;
        rigidbody.isKinematic = false;
        rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 1.5F, ForceMode.Impulse);


        //call SetIsFlying() after a delay
        Invoke("SetIsFlying", 1.0f);
    }

    void SetIsFlying()
    {
        isFlying = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Wall" && isFlying) ResetAgent();
    }

    void ResetAgent()
    {
        agent.enabled = true;
        rigidbody.isKinematic = true;
        isFlying = false;
    }

    public void ApplyDamage(float amount)
    {
        health -= amount;
        audioData.Play(0);
        if (health <= 0)
        {

            Die();
        }
    }

    void Die()
    {
        Ground.GetComponent<SpawnHandler>().EnemyKilled();
        Destroy(gameObject);
        //instantiate blood effect
        Vector3 bloodPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Instantiate(blood, bloodPosition, Quaternion.identity);
        Instantiate(blood2, bloodPosition, Quaternion.identity);

    }
}
