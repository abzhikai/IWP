using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Add the UI Variables
    [SerializeField] Slider PlayerHP;
    [SerializeField] GameObject player;

    int currentPlayerHP;
    int maxPlayerHP;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHP = player.GetComponent<PlayerStats>().GetHP();
        maxPlayerHP = player.GetComponent<PlayerStats>().GetMaxHP();
        PlayerHP.maxValue = maxPlayerHP;

        currentPlayerHP = maxPlayerHP;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHP.value = currentPlayerHP;
    }
}
