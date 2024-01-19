using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    [SerializeField] GameObject bulletprefab;
    [SerializeField] private SpriteRenderer spriteRenderer2D;

    public void Shoot(GameObject target)
    {
        //Debug.Log("enemy shoot called");
        Vector2 dir = target.transform.position - gameObject.transform.position;
        GameObject go = Instantiate(bulletprefab, transform.position, Quaternion.identity);
        EnemyBullet enemyBullet = go.GetComponent<EnemyBullet>();
        enemyBullet.bulletOwner = gameObject;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, -angle);
        go.GetComponent<Rigidbody2D>().velocity = dir.normalized * 50f;

        if (dir.x > 0)
            spriteRenderer2D.flipX = true;
        else if (dir.x < 0)
            spriteRenderer2D.flipX = false;
    }
}
