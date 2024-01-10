using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    IDLE,
    PATROLING,
    CHASING,
    ATTACKING,
    DEAD,
}

enum Direction
{
    LEFT,
    RIGHT,
}

public class GroundEnemy : MonoBehaviour
{
    int speed = 750, jumpHeight = 750;
    Animator animator;

    private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer2D;
    [SerializeField] private CircleCollider2D circleCollider2D;
    
    [SerializeField] GameObject groundDetection;
    // Left detection
    [SerializeField] Vector2 detectionPointA = new Vector2(-1, -0.5f);
    // Right detection
    [SerializeField] Vector2 detectionPointB = new Vector2(1, -0.5f);

    public Transform target;

    EnemyState currentEnemyState;
    Direction currentDirection;
    Vector2 direction;
    Vector2 force;

    bool doingAction;
    int dir;
    bool dirChange;
    float actionTimer;

    EnemyStats enemyStats;
    int dmgCooldown = 1;
    float dmgTimer = 0;
    bool knockback;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        doingAction = false;
        knockback = false;
    }

    void Update()
    {
        if (doingAction)
            actionTimer -= Time.deltaTime;

        // Enemy AI
        switch (currentEnemyState)
        {
            case EnemyState.IDLE:
                {
                    speed = 0;
                    if (!doingAction)
                    {
                        actionTimer = 2.0f;
                        doingAction = true;
                    }

                    if (actionTimer <= 0)
                    {
                        doingAction = false;
                        dir = CoinFlip();
                        currentEnemyState = EnemyState.PATROLING;
                    }

                    if (SightRange())
                        currentEnemyState = EnemyState.CHASING;
                }
                break;
            case EnemyState.PATROLING:
                {
                    speed = 600;
                    if (!doingAction)
                    {
                        actionTimer = 2.0f;
                        doingAction = true;

                        if (dir == 0)
                        {
                            currentDirection = Direction.LEFT;
                        }
                        else
                        {
                            currentDirection = Direction.RIGHT;
                        }
                    }

                    if (actionTimer <= 0)
                    {
                        doingAction = false;
                        currentEnemyState = EnemyState.IDLE;
                    }

                    if (SightRange())
                    {
                        currentEnemyState = EnemyState.CHASING;
                    }
                }
                break;
            case EnemyState.CHASING:
                {
                    speed = 750;
                    if (!SightRange())
                    {
                        currentEnemyState = EnemyState.IDLE;
                        doingAction = false;
                    }
                }
                break;
            case EnemyState.ATTACKING:
                {

                }
                break;
            case EnemyState.DEAD:
                {
                    Destroy(gameObject);
                }
                break;
        }
        //dirChange = EdgeCheck();
        //Debug.Log("Is Near Edge: " + EdgeCheck());
        if (EdgeCheck() == true)
        {
            if (currentDirection == Direction.LEFT)
            {
                currentDirection = Direction.RIGHT;
            }
            else
            {
                currentDirection = Direction.LEFT;
            }
        }
        animator.SetFloat("Speed", Mathf.Pow(rigidBody2D.velocity.magnitude, 0.5f), 0.1f, Time.deltaTime);

        if (enemyStats.damageTaken == true)
        {
            dmgTimer += Time.deltaTime;
            GameObject player =  GameObject.Find("Player");
            int playerATK = player.GetComponent<PlayerStats>().GetAtk();
            enemyStats.Damaged(playerATK);
            dmgTimer = 0;
            enemyStats.damageTaken = false;
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            direction = (Vector2)target.position - rigidBody2D.position;
        }
        else
        {
            if (currentDirection == Direction.LEFT)
            {
                Vector2 newTarget = rigidBody2D.position - new Vector2(1, 0);
                direction = newTarget - rigidBody2D.position;

            }
            else if (currentDirection == Direction.RIGHT)
            {
                Vector2 newTarget = rigidBody2D.position + new Vector2(1, 0);
                direction = newTarget - rigidBody2D.position;
            }
        }

        force = direction.normalized * speed * Time.deltaTime;

        // Update Animation
        if (force.x < 0)
        {
            spriteRenderer2D.flipX = false;
            currentDirection = Direction.LEFT;
        }
        else if (force.x > 0)
        {
            spriteRenderer2D.flipX = true;
            currentDirection = Direction.RIGHT;
        }

        rigidBody2D.AddForce(force);

        //if (dirChange)
        //{
        //    rigidBody2D.velocity = Vector2.zero;
        //}
    }

    bool SightRange()
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)transform.position, 20, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            // Set the target of the enemy
            target = collider.gameObject.transform;
            return true;
        }
        else
        {
            target = null;
            return false;
        }
    }

    bool EdgeCheck()
    {
        Vector3 groundDetectionEndPoint = groundDetection.transform.position - new Vector3(0, 3, 0);
        //Use player direction and the radius of the enemy collider to draw a line
        if (currentDirection == Direction.LEFT)
        {
            groundDetection.transform.localPosition = detectionPointA;
        }
        else if (currentDirection == Direction.RIGHT)
        {
            groundDetection.transform.localPosition = detectionPointB;
        }
        bool check = !Physics2D.Linecast(groundDetection.transform.position, groundDetectionEndPoint, ~LayerMask.GetMask("Terrain"));

        return check;
    }

    int CoinFlip()
    {
        return Random.Range(0, 2);
    }
}
