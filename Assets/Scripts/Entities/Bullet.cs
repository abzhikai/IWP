using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Terrain"))
        {
            Debug.Log("Bullet hit object");
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyStats>().Damage();
                Debug.Log("Enemy is hit");
            }
            Destroy(gameObject);
        }
    }
}
