using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public string itemName; // The name of the item (or any unique identifier)
    public int count; // The count of that item

    public InventoryItemData(string itemName, int count)
    {
        this.itemName = itemName;
        this.count = count;
    }
}