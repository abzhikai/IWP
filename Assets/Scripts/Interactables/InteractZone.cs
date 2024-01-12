using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractZone : MonoBehaviour
{
    bool playerInRange;

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
        GetComponentInParent<Interactable>().SetCanInteract(playerInRange);
    }
}
