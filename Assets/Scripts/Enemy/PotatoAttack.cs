using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoAttack : MonoBehaviour
{
    EnemyStats enemyStats;
    float attackCooldown = 1;
    float attackInterval;
    bool attacked;

    GameObject player;
    void Start()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
    }

    void Update()
    {
        attackInterval += Time.deltaTime;
        if (attacked == true)
        {
            if (attackInterval >= attackCooldown)
            {
                player.GetComponent<PlayerStats>().Damaged(enemyStats.GetAtk());
                attackInterval = 0;
                attacked = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && attacked == false)
        {
            player = other.gameObject;
            attacked = true;
        }
    }
}
