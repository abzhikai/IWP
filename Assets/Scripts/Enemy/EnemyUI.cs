using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    // Add the UI Variables
    [SerializeField] Slider EnemyHP;
    [SerializeField] GameObject enemy;

    int currentEnemyHP;
    int maxEnemyHP;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHP = enemy.GetComponent<EnemyStats>().GetHP();
        maxEnemyHP = enemy.GetComponent<EnemyStats>().GetMaxHP();
        EnemyHP.maxValue = maxEnemyHP;

        currentEnemyHP = maxEnemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemyHP = enemy.GetComponent<EnemyStats>().GetHP();
        EnemyHP.value = currentEnemyHP;
    }
}
