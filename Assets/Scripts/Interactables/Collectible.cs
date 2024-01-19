using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] string itemName;
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerInventory playerInventory = other.gameObject.GetComponent<PlayerInventory>();
        if (playerInventory != null && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
            playerInventory.AddItem(itemName, 1);
        }
        //Debug.Log("Inventory item: " + playerInventory.GetItemAmount(itemName));
        //Debug.Log(playerInventory);
    }
}
