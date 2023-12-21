using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject bulletOwner;
    //[SerializeField] Vector2 knockbackForce;
    void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>()?.Damaged(bulletOwner.GetComponent<EnemyStats>().GetAtk());
            //other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce);
            Destroy(gameObject);

        }
    }
}
