using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    EnemyStats enemyStats;
    Vector2 knockbackForce;

    enum MeleeDir
    {
        LEFT,
        RIGHT,
    }

    void Start()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
    }
    //[SerializeField] Vector2 knockbackForce;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerStats>().Damaged(enemyStats.GetAtk());
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce);
            }
        }
    }
}
