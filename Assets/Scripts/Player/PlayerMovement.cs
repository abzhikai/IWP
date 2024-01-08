using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] Animator animator;

    private bool isMidAir;
    private bool jump;
    //bool jumping = false;
    //bool secondCheck = false;

    //Transform t;

    private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer2D;

    float dirX;
    float dirY;

    // Start is called before the first frame update
    void Start()
    {
        //t = GetComponent<Transform>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called every frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        //if (rigidBody2D.velocity.y < 0 || rigidBody2D.velocity.y > 0)
        //{
        //    isMidAir = true;
        //}

        // Sprite flip
        if (dirX > 0)
        {
            spriteRenderer2D.flipX = true;
            //Debug.Log("SpriteFlipped");
        }
        else if (dirX < 0)
        {
            spriteRenderer2D.flipX = false;
            //Debug.Log("SpriteFlipped");
        }

        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        //{
        //    if (isMidAir == false)
        //    {
        //        jump = true;
        //        //Debug.Log("jump");
        //    }
        //}
        jump = (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && (isMidAir == false);

        if (jump == true)
        {
            JumpMovement();
        }

        //// Attack button
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{

        //}

        animator.SetFloat("Speed", Mathf.Pow(rigidBody2D.velocity.magnitude, 0.5f), 0.1f, Time.deltaTime);

    }

    // FixedUpdate is used for physics calculation
    void FixedUpdate()
    {
        Physics2D.queriesHitTriggers = false;
        // Jump Check
        isMidAir = !Physics2D.Raycast(rigidBody2D.position, -Vector2.up, 0.1f + (GetComponent<Collider2D>().bounds.extents.y), ~LayerMask.GetMask("Player"));
        
        // Movement
        rigidBody2D.velocity = new Vector2(dirX * speed, rigidBody2D.velocity.y);
        //Debug.Log(isMidAir);
    }

    void JumpMovement()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpHeight);
        //jump = false;
    }

}
