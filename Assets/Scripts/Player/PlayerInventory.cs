using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    Dictionary<string, int> inventoryItems = new Dictionary<string, int>();

    public void AddItem(string item, int amount)
    {
        if (inventoryItems.ContainsKey(item))
        {
            inventoryItems[item] += amount;
        }
        else
        {
            inventoryItems[item] = amount;
        }
    }

    public int GetItemAmount(string item)
    {
        if (!inventoryItems.ContainsKey(item))
        {
            return 0;
        }
        else
        {
            return inventoryItems[item];
        }
    }
}
