using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool canInteract;
    //GameManager gameManager = GameManager.instance;
    // Start is called before the first frame update
    void Start()
    {
        canInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F) && canInteract)
        //{
        //    gameManager.NoticeUpdate();
        //}
    }

    public bool GetCanInteract()
    {
        return canInteract;
    }

    public void SetCanInteract(bool val)
    {
        canInteract = val;
    }
}
