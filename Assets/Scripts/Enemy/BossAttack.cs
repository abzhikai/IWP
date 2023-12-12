using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    enum AttackType
    {
        SHOOTING,
        MELEE,
    }

    AttackType currentAttackType;

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
        //if (currentAttackType == AttackType.SHOOTING)
        //    attackCooldown = 0.7f;
        //else if (currentAttackType == AttackType.MELEE)
        //    attackCooldown = 1.0f;
    }

    void FixedUpdate()
    {

    }

    public void Shoot(GameObject target)
    {
        Vector2 dir = target.transform.position;
        GameObject go = Instantiate(bulletprefab, transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, -angle);
        go.GetComponent<Rigidbody2D>().velocity = dir * 50f;

        if (dir.x > 0)
            spriteRenderer2D.flipX = true;
        else if (dir.x < 0)
            spriteRenderer2D.flipX = false;
    }

    public void Melee(GameObject target)
    {
            Vector3 pos = meleeattack.transform.localPosition;

            Vector2 dir = target.transform.position;
            if (dir.x < 0)
            {
                meleeattack.transform.localPosition = new Vector3(-1, pos.y, pos.z);
                meleeattack.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }
            else if (dir.x > 0)
            {
                meleeattack.transform.localPosition = new Vector3(1, pos.y, pos.z);
                meleeattack.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            StartCoroutine(Melee());
    }
    
    IEnumerator Melee()
    {        
        meleeattack.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        meleeattack.SetActive(false);
    }

}
