using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    Vector2 knockbackForce = new Vector2(1, 1);

    enum MeleeDir
    {
        LEFT,
        RIGHT,
    }

    MeleeDir meleeDir;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyStats>().damageTaken = true;
                if (meleeDir == MeleeDir.LEFT)
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 15, ForceMode2D.Impulse);
                }
                else
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 15, ForceMode2D.Impulse);
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
