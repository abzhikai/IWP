using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    int speed, jumpHeight;
    Animator animator;

    private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer2D;

    public Transform target;
    Path path;
    int currentWaypoint = 0;
    float nextWaypointDistance = 3f;
    bool reachEndOfPath = false;
    Seeker seeker;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidBody2D.position, target.position, OnPathComplete);
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
        Vector2 direction = (Vector2)path.vectorPath[currentWaypoint] - rigidBody2D.position.normalized;
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
}
