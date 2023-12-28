using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject bulletprefab;
    [SerializeField] GameObject meleeattack;
    [SerializeField] private SpriteRenderer spriteRenderer2D;

    // Start is called before the first frame update
    void Start()
    {
        meleeattack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

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

    public void Melee(GameObject target)
    {
        //Debug.Log("enemy melee called");
        Vector3 pos = meleeattack.transform.localPosition;

        Vector3 dir = target.transform.position - transform.position; // Calculate direction relative to the enemy
        dir.Normalize(); // Normalize the direction vector

        meleeattack.transform.localPosition = new Vector3(dir.x, pos.y, pos.z);

        // Set flipX based on the direction
        meleeattack.GetComponentInChildren<SpriteRenderer>().flipX = (dir.x > 0);

        meleeattack.SetActive(true);
        StartCoroutine(MeleeActive());

        //Debug.Log("enemy melee called");
        //Vector3 pos = meleeattack.transform.localPosition;

        //Vector2 dir = target.transform.position;
        //if (dir.x < 0)
        //{
        //    meleeattack.transform.localPosition = new Vector3(-1, pos.y, pos.z);
        //    meleeattack.GetComponentInChildren<SpriteRenderer>().flipX = false;
        //}
        //else if (dir.x > 0)
        //{
        //    meleeattack.transform.localPosition = new Vector3(1, pos.y, pos.z);
        //    meleeattack.GetComponentInChildren<SpriteRenderer>().flipX = true;
        //}
        //meleeattack.SetActive(true);
        //StartCoroutine(MeleeActive());
    }
    
    IEnumerator MeleeActive()
    {        
        yield return new WaitForSeconds(0.7f);
        meleeattack.SetActive(false);
    }

}
