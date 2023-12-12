using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    //[SerializeField] Vector2 knockbackForce;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Debug.Log("Melee hit object");
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyStats>().damageTaken = true;
                //other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce);
            }
        }
    }
}
