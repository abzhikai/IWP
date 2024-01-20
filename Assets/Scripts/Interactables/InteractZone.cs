using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractZone : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    bool playerInRange;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = true;
            indicator.SetActive(true);
        }
        else
        {
            playerInRange = false;
        }
        GetComponentInParent<Interactable>().SetCanInteract(playerInRange);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = false;
            indicator.SetActive(false);
        }
    }

    public bool getPlayerInRange()
    {
        return playerInRange;
    }
}
