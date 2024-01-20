using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RED_ship : Interactable
{
    GameManager gameManager;
    GameObject player;
    InteractZone interactZone;

    [SerializeField] GameObject gameWinPanel;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameManager.instance;
        interactZone = gameObject.GetComponentInChildren<InteractZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && GetCanInteract() && interactZone.getPlayerInRange())
        {
            if (player.GetComponent<PlayerInventory>().GetItemAmount("Fuel") >= 1)
            {
                gameWinPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                gameManager.NoticeUpdate("Objective: Obtain Fuel");
            }
        }
    }
}
