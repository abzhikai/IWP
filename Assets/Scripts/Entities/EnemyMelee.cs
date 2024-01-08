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

    MeleeDir meleeDir;

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
                if (meleeDir == MeleeDir.LEFT)
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 20, ForceMode2D.Impulse);
                }
                else
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20, ForceMode2D.Impulse);
                }
            }
        }
    }

    public void SetDirToLEFT()
    {
        meleeDir = MeleeDir.LEFT;
    }

    public void SetDirToRIGHT()
    {
        meleeDir = MeleeDir.RIGHT;
    }
}
