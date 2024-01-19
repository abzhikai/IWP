using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject item;
    EnemyStats enemyStats;
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStats != null)
        {
            if (enemyStats.GetHP() <= 0)
            {
                GameObject go = Instantiate(item, transform.position, Quaternion.identity);
            }
        }
    }
}
