using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLUE_pod : Interactable
{
    GameManager gameManager;
    InteractZone interactZone;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        interactZone = gameObject.GetComponentInChildren<InteractZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && GetCanInteract() && interactZone.getPlayerInRange())
        {
            gameManager.NoticeUpdate("Escape Pod: Belongs to Brax");
        }
    }
}
