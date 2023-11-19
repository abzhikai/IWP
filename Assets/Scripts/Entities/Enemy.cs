using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    int HP, ATK, speed, jumpHeight;
    Animator animator;

    private Rigidbody2D rigidBody2D;
    [SerializeField] private SpriteRenderer spriteRenderer2D;

    void Update()
    {
        // Update Animation
    }
}
