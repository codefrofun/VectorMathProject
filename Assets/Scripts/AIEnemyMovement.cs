using UnityEngine;
using UnityEngine.UI;

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

    public UiController gameOverUIController;
    public Button restartButton;

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
        Debug.DrawRay(transform.position, direction * 2f, Color.green);
        movement = direction * moveSpeed;

        float dotProduct = Vector2.Dot(transform.up, direction);

        if(dotProduct > 0)
        {
            movement = direction * moveSpeed;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking player");
        float distanceToPlayer = (player.position - transform.position).magnitude;
        if (distanceToPlayer <= 0.1f)
        {
            gameOverUIController.ShowGameOver();
            movement = Vector2.zero;
            restartButton.gameObject.SetActive(true);
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float distanceToPlayer = (other.transform.position - transform.position).magnitude;

            if (distanceToPlayer <= attackRange)
            {
                Debug.Log("Enemy collided with player and is now attacking");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}