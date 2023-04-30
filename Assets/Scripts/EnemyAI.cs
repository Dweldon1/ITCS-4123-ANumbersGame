using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;


    // Patrol Variables
    public Vector3 patrolPoint;
    public bool patrolPointSet;
    public float patrolPointRange;

    // Attack variables
    public float attackTimer;
    bool alreadyAttacked;
    public bool hasDied;

    // AI States
    public float viewRange, attackRange;
    public bool playerInView, attackPlayer;

    // Speed variables
    public float walk = 2f;
    public float run = 4f;

    private Animator animator;



    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hasDied = false;
    }

    private void Update()
    {
        // Check is player is visible and in attack range
        playerInView = Physics.CheckSphere(transform.position, viewRange, playerMask);
        attackPlayer = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!playerInView && !attackPlayer)
        {
            OnPatrol();
            agent.speed = walk;
        }
        if (playerInView && !attackPlayer)
        {
            Chase();
            agent.speed = run;
        }
        if (playerInView && attackPlayer)
        {
            Attack();
            agent.speed = 0;
        }


        Animations();
        
    }

    private void OnPatrol()
    {
        if (!patrolPointSet)
        {
            SearchPatrolPoint();
        }

        if (patrolPointSet)
        {
            agent.SetDestination(patrolPoint);
        }

        Vector3 disToPatrolPoint = transform.position - patrolPoint;

        // point reached
        if (disToPatrolPoint.magnitude < 2f)
        {
            patrolPointSet = false;
        }
    }

    private void SearchPatrolPoint()
    {
        float randomX = Random.Range(-patrolPointRange, patrolPointRange);
        float randomZ = Random.Range(-patrolPointRange, patrolPointRange);

        patrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(patrolPoint, -transform.up, 2f, groundMask))
        {
            patrolPointSet = true;
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        if (transform.position.y >= player.position.y)
        {
            transform.LookAt(player);
        }

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackTimer);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Animations()
    {
        if (agent.speed == 0)
        {
            animator.SetBool("Attacking", true);
            animator.SetBool("Chasing", false);
        } 

        if (agent.speed == walk)
        {
            animator.SetBool("Patroling", true);
        }
        else
        {
            animator.SetBool("Patroling", false);
        }

        if (agent.speed == run)
        {
            animator.SetBool("Chasing", true);
            animator.SetBool("Attacking", false);
        }
        else
        {
            animator.SetBool("Chasing", false);
        }
        if (hasDied) {
            animator.SetBool("JumpedOn", true);
            animator.SetBool("Patroling", false);
            animator.SetBool("Chasing", false);
        }
    }
}
