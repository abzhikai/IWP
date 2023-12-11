using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]int HP = 25, ATK = 5;

    bool damageTaken;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage()
    {
        damageTaken = true;
    }

    public int GetEnemyAtk()
    {
        return ATK;
    }

}
