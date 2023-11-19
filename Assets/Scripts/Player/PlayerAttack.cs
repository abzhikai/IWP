using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    float attackCooldown = 1;
    bool canAttack = true;

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

        // Controls the attack type
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0f)
        {
            Debug.Log("Scroll Up");
            // Scrolled up
            if (currentAttackType > AttackType.SHOOTING)
                currentAttackType--;
        }
        else if (scrollInput < 0f)
        {
            Debug.Log("Scroll Down");
            // Scrolled down
            if (currentAttackType < AttackType.MELEE)
                currentAttackType++;
        }
        if (Input.GetMouseButton(0) && canAttack)
        {
            if (currentAttackType == AttackType.SHOOTING)
            {
                Shoot();
            }
            else if (currentAttackType == AttackType.MELEE)
            {
                StartCoroutine(Melee());
            }

            canAttack = false;

            StartCoroutine(ResetAttackCooldown());
        }
    }

    void FixedUpdate()
    {
        
    }

    void Shoot()
    {
        Vector2 dir = PlayerToMouseDir();
        GameObject go = Instantiate(bulletprefab, transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, -angle);
        go.GetComponent<Rigidbody2D>().velocity = dir * 50f;

        if (dir.x > 0)
            spriteRenderer2D.flipX = true;
        else if (dir.x < 0)
            spriteRenderer2D.flipX = false;
    }

    IEnumerator Melee()
    {
        // Get mouse position
        // Use mouse postion to rotate the melee hitbox
        // Offset the gameobject
        Vector3 pos = meleeattack.transform.localPosition;

        Vector2 dir = PlayerToMouseDir();
        if (dir.x < 0)
        {
            //go.GetComponent<BoxCollider2D>().offset = new Vector2(-BoxOffset, 0);
            //Debug.Log(go.GetComponent<BoxCollider2D>().offset.x);
            //go.GetComponentInChildren<SpriteRenderer>().flipX = false;
            meleeattack.transform.localPosition = new Vector3(-1, pos.y, pos.z);
            //meleeattack.transform.eulerAngles = new Vector3(0, 0, 0);
            meleeattack.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (dir.x > 0)
        {
            //go.GetComponent<BoxCollider2D>().offset = new Vector2(BoxOffset, 0);
            //Debug.Log(go.GetComponent<BoxCollider2D>().offset.x);
            //go.GetComponentInChildren<SpriteRenderer>().flipX = true;
            meleeattack.transform.localPosition = new Vector3(1, pos.y, pos.z);
            //meleeattack.transform.eulerAngles = new Vector3(0, 180, 0);
            meleeattack.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        meleeattack.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        meleeattack.SetActive(false);
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    Vector2 PlayerToMouseDir()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        return dir;
    }
}
