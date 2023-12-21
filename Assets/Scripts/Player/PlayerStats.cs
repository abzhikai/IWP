using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]int maxHP = 100, HP, ATK = 5;

    public bool damageTaken;
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damaged(int dmg)
    {
        HP -= dmg;
        damageTaken = true;
    }

    public int GetAtk()
    {
        return ATK;
    }

    public int GetHP()
    {
        return HP;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
}
