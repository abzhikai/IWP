using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]int HP = 100, ATK = 5;

    bool damageTaken;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        damageTaken = true;

    }

    public int GetPlayerAtk()
    {
        return ATK;
    }
}
