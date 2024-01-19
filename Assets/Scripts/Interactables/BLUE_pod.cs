using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLUE_pod : Interactable
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && GetCanInteract())
        {
            gameManager.NoticeUpdate("Escape Pod: Belongs to Brax");
        }
    }
}
