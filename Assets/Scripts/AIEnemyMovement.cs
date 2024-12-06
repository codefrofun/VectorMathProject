using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AIEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public Transform player;
    public Transform patrolPointA;
    public Transform patrolPointB;
    public float patrolSpeed = 1f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isChasing = false;
    private bool isAttacking = false;
    private bool isPatrolling = false;

    private void Start()
    {
        {
            rb = GetComponent<Rigidbody2D>();
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }

    private void Update()
    {
        // using magnitude 
        float distanceToPlayer = (player.position - transform.position).magnitude;

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            isPatrolling = false;
        }
        else
        {
            isChasing = false;
            isPatrolling = true;
        }

        if (distanceToPlayer <= attackRange)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (isAttacking)
        {
            AttackPlayer();
        }
        else if(isChasing)
        {
            ChasePLayer();
        }
        else if(isPatrolling)
        {
            Patrol();
        }

        rb.velocity = movement;
    }

    // vector direction
    void Patrol()
    {
        Vector2 target = (Vector2)patrolPointA.position;

        if(Vector2.Distance(transform.position, patrolPointA.position) < 0.1f)
        {
            target = (Vector2)patrolPointB.position;
        }
        else if (Vector2.Distance(transform.position, patrolPointB.position) < 0.1f)
        {
            target = (Vector2)patrolPointA.position;
        }

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        movement = direction * patrolSpeed;
    }

    //Dot product
    void ChasePLayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        float dotProduct = Vector2.Dot(transform.up, direction);

        if(dotProduct > 0)
        {
            movement = direction * moveSpeed;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking player");
        //You have been caught! message displayed
        //restart button
        // reset player to start psoition (in dungeon)
        movement = Vector2.zero;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy collided with player and is now attacking");
    }
}