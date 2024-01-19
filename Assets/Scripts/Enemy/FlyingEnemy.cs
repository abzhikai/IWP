using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    int speed = 100;
    Animator animator;

    private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer2D;

    public Transform target;
    Path path;
    int currentWaypoint = 0;
    float nextWaypointDistance = 3f;
    bool reachEndOfPath = false;
    Seeker seeker;

    EnemyState currentEnemyState;

    bool doingAction;
    int dir;
    bool dirChange;
    float actionTimer;

    EnemyStats enemyStats;
    int dmgCooldown = 1;
    float dmgTimer = 0;
    bool knockback;

    float attackCooldown = 3f;
    bool canAttack = true;
    float attackTimer = 0;

    bool startChase;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        doingAction = false;

        //InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (target != null)
            {
                seeker.StartPath(rigidBody2D.position, target.position, OnPathComplete);
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (doingAction)
            actionTimer -= Time.deltaTime;

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            canAttack = true;
        }

        switch(currentEnemyState)
        {
            case EnemyState.IDLE:
                {
                    speed = 0;

                    if (SightRange())
                    {
                        currentEnemyState = EnemyState.CHASING;
                        InvokeRepeating("UpdatePath", 0f, 0.5f);
                    }
                }
                break;
            case EnemyState.CHASING:
                {
                    speed = 100;
                    if (SightRange())
                    {
                        currentEnemyState = EnemyState.ATTACKING;
                    }
                }
                break;
            case EnemyState.ATTACKING:
                {
                    speed = 0;
                    if(canAttack == true)
                    {
                        GameObject player = GameObject.Find("Player");
                        gameObject.GetComponent<DroneAttack>().Shoot(player);
                        attackTimer = 0;
                        canAttack = false;
                    }
                    if (!SightRange())
                    {
                        currentEnemyState = EnemyState.CHASING;
                    }
                }
                break;
            case EnemyState.DEAD:
                {
                    Destroy(gameObject);
                }
                break;
        }

        //if (SightRange())
        //{
        //    speed = 0;
        //    startChase = true;
        //}
        //else
        //{
        //    speed = 100;
        //}

        if (enemyStats.damageTaken == true)
        {
            dmgTimer += Time.deltaTime;
            GameObject player = GameObject.Find("Player");
            int playerATK = player.GetComponent<PlayerStats>().GetAtk();
            enemyStats.Damaged(playerATK);
            Debug.Log("Dealt DMG");
            dmgTimer = 0;
            enemyStats.damageTaken = false;
        }
    }

    void FixedUpdate()
    {
        // Update Path
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            return;
        }
        else
        {
            reachEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody2D.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rigidBody2D.AddForce(force);

        float distance = Vector2.Distance(rigidBody2D.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Update Animation
        if (force.x < 0)
        {
            spriteRenderer2D.flipX = false;
        }
        else if (force.x > 0)
        {
            spriteRenderer2D.flipX = true;
        }

    }

    bool SightRange()
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)transform.position, 15, LayerMask.GetMask("Player"));
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
}
