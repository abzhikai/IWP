using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 knockbackForce;
    void Start()
    {
        knockbackForce = GetComponent<Rigidbody2D>().velocity;
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Physics2D.queriesHitTriggers = false;
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Debug.Log("Bullet hit object");
            
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyStats>().damageTaken = true;
                if (other.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce * 10);
                }
            }
            Destroy(gameObject);
        }
    }
}
